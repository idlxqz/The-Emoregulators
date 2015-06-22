// RemoteCharacter.cs - 
//
// Copyright (C) 2006 GAIPS/INESC-ID
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
// Company: GAIPS/INESC-ID
// Project: FearNot!
// Created: 06/04/2006
// Created by: João Dias
// Email to: joao.dias@inesc-id.pt
// 
// History:
// João Dias: 24/09/2006 - File Created
//

using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

using ION.Core.Events;
using ION.Core.Extensions;
using ION.Core.Extensions.Events;
using ION.Meta;
using ION.Meta.Events;
//using Unity.Realizer;



using System.IO;
using System;

namespace FAtiMA.RemoteAgent
{
    public class RemoteMind
    {
        private const string EOM_TAG = "\n";
        private const string EOF_TAG = "<EOF>";
        private const int bufferSize = 1024;


        public const string REMOTE_ACTION_PARAMETERS = "REMOTE_ACTION_PARAMETERS";

        public const string START_MESSAGE = "CMD Start";
        public const string STOP_MESSAGE = "CMD Stop";
        public const string RESET_MESSAGE = "CMD Reset";
        public const string SAVE_MESSAGE = "CMD Save";
        public const string REMOVE_ALL_GOALS_MESSAGE = "REMOVEALLGOALS";
        public const string ADD_GOALS_MESSAGE_START = "ADDGOALS ";
        public const string ACTION_FINISHED = "ACTION-FINISHED";
        public const string ACTION_STARTED = "ACTION-STARTED";
        public const string ACTION_FAILED = "ACTION-FAILED";
        public const string ENTITY_ADDED = "ENTITY-ADDED";
        public const string ENTITY_REMOVED = "ENTITY-REMOVED";
        public const string PROPERTY_CHANGED = "PROPERTY-CHANGED";
        public const string PROPERTY_REMOVED = "PROPERTY-REMOVED";
        public const string USER_SPEECH = "USER-SPEECH";
        public const string AGENTS = "AGENTS";
        public const string LOOK_AT = "LOOK-AT";
        public const string ADVANCE_TIME = "ADVANCE-TIME";
        public const string STOP_TIME = "STOP-TIME";
        public const string RESUME_TIME = "RESUME-TIME";
        public const string SHUTDOWN_MESSAGE = "SHUTDOWN";

        //communication/socket fields
        private Socket socket;
        private NetworkStream socketStream;
        private bool receiverAlive;
        private Thread receiverThread;
//        private byte[] buffer = new byte[bufferSize];
		
		// Henrique Campos - auxiliary variable for emotional state messages in Parse(..)
		private string _previousEmotionalMsg = String.Empty;


        //look-at hack
        private LookAT lookATAction;

        //properties
        //public Property<EmotionalState> emotionalState;
        //protected Property<RelationSet> relations;
        public EntityProperty<string> Sex {get ; private set;}
        public EntityProperty<string> Role { get; private set; }
		// Henrique Campos - emotional state property
		public EntityProperty<EmotionalState> EmotionalState { get; private set; }
        public Entity Body { get; private set; }
		public bool ConnectionReady {get; set;}
        //actions  
       
        public RemoteMind(Entity body, string sex, string role) 
		{
            this.Body = body;
            this.receiverAlive = false;
			ConnectionReady = false;
            //this.emotionalState = new Property<EmotionalState>(new EmotionalState());
            //this.relations = new Property<RelationSet>(new RelationSet());
            this.Sex = new EntityProperty<string>(this.Body,"sex",sex);
            this.Role = new EntityProperty<string>(this.Body, "role", role);
			// Henrique Campos - emotional state property
			this.EmotionalState = new EntityProperty<EmotionalState>(this.Body, "emotionalstate", new EmotionalState());
            this.Body.AddProperty("*",this.Sex);
            this.Body.AddProperty("*",this.Role);
			// Henrique Campos - emotional state property
			this.Body.AddProperty("*",this.EmotionalState);
			
            this.lookATAction = new LookAT(this.Body,this);
            this.Body.AddAction(this.lookATAction);
            //this.Body.AddToSimulation(Simulation.Instance); //Christopher: why adding the body entity twice, here and in IONEntity?

            Simulation.Instance.EventHandlers.Add<IAddedTo<Entity, Simulation>>(this.EntityAdded);
			
			//Henrique Campos - added this event handler
			//Christopher: removed this at is never invoked and only causes strange errors; use RemoyeMind.SendCMDShutdown() instead; entity will be removed from sim by IONEntity
			//Simulation.Instance.EventHandlers.Add<IElementRemovedFromSimulation>(this.OnRemovedFromSimulation);

            //Simulation.Instance.EventHandlers.Add(new DelegateEventHandler<IRemoved<Entity, Simulation>>(this.EntityRemoved));
            
		}

        /*public override void AddToSimulation(Simulation s)
        {
            //base.AddToSimulation(s);
            //s.Elements.Add(emotionalState);
            //s.Elements.Add(loadAction);
        }*/

        ~RemoteMind()
        {
			this.OnDestroy();
        }


        public Socket Socket
        {
            get
            {
                return this.socket;
            }
            set
            {
                this.socket = value;
                this.socketStream = new NetworkStream(this.socket);
            }
        }

        public bool ReceiverAlive
        {
            get
            {
                return this.receiverAlive;
            }
            set
            {
                this.receiverAlive = value;
            }
        }

        public void Start()
        {
            Entity ent;
            string agents = "";
			List<string> agentsList = new List<string>();
            foreach (Element e in Simulation.Instance.Elements)
            {
                if (e is Entity)
                {
                    ent = e as Entity;
                    //agents += " " + ent.Name;
					agentsList.Add(ent.Name);
                    RegisterEventListeners(e);
                }
            }
			
			System.Random rnd = new System.Random();
			while(agentsList.Count > 0){
				int pick = rnd.Next(agentsList.Count);
				string nme = agentsList[pick];
				agents += " " + nme;
				agentsList.Remove(nme);
			}
			
			Debug.Log("Randomised List of entities and agents for " + this.Body.Name + ": [" + agents + "]");

            this.Send("OK");
            this.receiverAlive = true;

            this.receiverThread = new Thread(new ThreadStart(ReceiveThread));
            this.receiverThread.Start();
			
			ApplicationLogger.Instance().WriteLine("sending AGENTS MESSAGE to " + this.Body.Name + ": AGENTS " + agents);

            this.Send("AGENTS" + agents);
			
			this.Body.Raise(new InitialisationFinishedEvent(this.Body, this.Body.Name));
        }

        // MARCO: ADD BASE OnDestroy
        public void OnDestroy()
		{
			
			ApplicationLogger.Instance().WriteLine("AGENT ENTITY: " + this.Body.Name + " was destroyed...");
			
			//removeEventHandlers();
			
            if (this.socketStream != null)
            {
                if (receiverAlive)
                {
					this.receiverAlive = false;
                    this.Send(SHUTDOWN_MESSAGE); //Makes the java mind close the socket
                }
                socketStream.Close();
            }

            if (this.socket != null)
            {
                try
                {
                    socket.Close();
                }
                catch(Exception)
                {
                }
            }
        }
		
		/// <summary>
		/// Removes the event handlers from other simulation entities and simulation itself
		/// </summary>
		// Christopher: added this for proper entity removal
		public void removeEventHandlers(/*void*/){
			
			foreach (Element e in Simulation.Instance.Elements)
            {
                if (e is Entity)
                {
                    e.EventHandlers.Remove<IStarted>(this.ActionStarted);
            		e.EventHandlers.Remove<ISucceeded>(this.ActionSucceeded);
            		e.EventHandlers.Remove<IFailed>(this.ActionFailed);

            		e.EventHandlers.Remove<IPropertyAdded>(this.PropertyAdded);            		
            		e.EventHandlers.Remove<IRestrictedPropertyChange>(this.RestrictedPropertyChange);
                }
			}
				
			Simulation.Instance.EventHandlers.Add<IAddedTo<Entity, Simulation>>(this.EntityAdded);					
			//Simulation.Instance.EventHandlers.Add<IElementRemovedFromSimulation>(this.OnRemovedFromSimulation);           
		
		}/*END OF removeEventHandlers(void)*/
		
		// Henrique Campos - added this to be called on the event of being removed from simulation
		public void OnRemovedFromSimulation(IElementRemovedFromSimulation evt){
			
			
			if(evt.Element.UID == this.Body.UID){				
				ApplicationLogger.Instance().WriteLine("AGENT ENTITY: " + this.Body.Name + " was removed from simulation ...");
				this.OnDestroy();
			}
		}

        private void Parse(String msg)
        {
            ActionParameters parameters;

            if (msg.StartsWith(PROPERTY_CHANGED))
            {
                /*string[] aux = msg.Split(' ');
                string propertyName = aux[2];
                string value = aux[3];
                if(this.HasProperty<Property<String>>(propertyName))
                {
                    Property<String> p = this.GetProperty<Property<String>>(propertyName);
                    if (!p.Value.Equals(value))
                    {
                        p.Value = value;
                    }
                }*/
            }
            else if (msg.StartsWith("<EmotionalState"))
            {
				
				//Debug.Log("<RemoteMind> Received emotional state: " + msg);
				// Henrique Campos - emotional state property update
				if(_previousEmotionalMsg.CompareTo(msg) != 0){
					_previousEmotionalMsg = msg;
					
					EmotionalState es = (EmotionalState)EmotionalStateParser.Instance.Parse(msg);
					ApplicationLogger.Instance().WriteLine(this.Body.Name + " mood is " + es.Mood);
					this.Body.SetProperty<EmotionalState>("SELF", "emotionalstate", es);
				}
            }
            else if (msg.StartsWith("<Relations"))
            {
            }
            else if (msg.StartsWith("look-at"))
            {
                string[] aux = msg.Split(' ');
                //LookAt(aux[1]);
                parameters = new ActionParameters();
                parameters.Subject = this.Body.Name;
                parameters.ActionType = "look-at";
                parameters.Target = aux[1];
				
				//Thread.Sleep(1000);
				
                this.lookATAction.Start(parameters);
            }
            else if (msg.StartsWith("<SpeechAct"))
            {
				ApplicationLogger.Instance().WriteLine(this.Body.Name + " received speech act request from FAtiMA side " + msg);
				SpeechActParameters speechParams = (SpeechActParameters) SpeechActParser.Instance.Parse(msg);
                
                EntityAction<ActionParameters> a = this.Body.getActionByName("SpeechAct") as EntityAction<ActionParameters>;
                if (a != null)
                {
					ApplicationLogger.Instance().WriteLine(this.Body.Name + " starting SpeechAct " + speechParams.ToXML()); 
                    a.Start(speechParams);
                }
            }
            else if (msg.StartsWith("<Action"))
            {
                ApplicationLogger.Instance().WriteLine(this.Body.Name + " received action request from FAtiMA side " + msg);
                parameters = (ActionParameters) ActionParametersParser.Instance.Parse(msg);
                EntityAction<ActionParameters> a = this.Body.getActionByName(parameters.ActionType) as EntityAction<ActionParameters>;
                if (a != null)
                {
					ApplicationLogger.Instance().WriteLine(this.Body.Name + " starting Action " + parameters.ToXML());
                    a.Start(parameters);
                }
            }
        }
		
		public void SendCMDStart()
		{
			ApplicationLogger.Instance().WriteLine("-- CMD Start sent to FAtiMA Agent --");
			if(this.receiverAlive)
			{
				Send(START_MESSAGE);
			}
		}
		
		public void SendCMDStop()
		{
			ApplicationLogger.Instance().WriteLine("-- CMD Stop sent to FAtiMA Agent --");
			if(this.receiverAlive)
			{
				Send(STOP_MESSAGE);
			}
		}
		
		public void SendCMDSave()
		{
			ApplicationLogger.Instance().WriteLine("-- CMD Save sent to FAtiMA Agent --");
			if(this.receiverAlive)
			{
				Send(SAVE_MESSAGE);
			}
		}
		
		public void SendCMDStopTime()
		{
			ApplicationLogger.Instance().WriteLine("-- CMD Stop Time sent to FAtiMA Agent --");
			if(this.receiverAlive)
			{
				Send(STOP_TIME);
			}
		}
		
		public void SendCMDResumeTime()
		{
			ApplicationLogger.Instance().WriteLine("-- CMD Resume Time sent to FAtiMA Agent --");
			if(this.receiverAlive)
			{
				Send(RESUME_TIME);
			}
		}
		
		public void SendCMDShutdown()
		{
			ApplicationLogger.Instance().WriteLine("-- CMD Shutdown sent to FAtiMA Agent --");			
			this.OnDestroy();			
		}

        /*private void LookAt(string entityName)
        {
            Element e;
            Entity ne;
             ApplicationLogger.Instance().WriteLine(this.Body.Name + " looks at " + entityName);
            string msg =  LOOK_AT + " " + entityName;

            e = Simulation.Instance.Elements[Entity.getIDByName(entityName)];

            if(e is Entity)
            {
                ne = e as Entity;
                msg += " " + ne.getStringWithAllProperties();
            }
            

            Send(msg);
            
        }*/

        private void RegisterEventListeners(Element e)
        {
            e.EventHandlers.Add<IStarted>(this.ActionStarted);
            e.EventHandlers.Add<ISucceeded>(this.ActionSucceeded);
            e.EventHandlers.Add<IFailed>(this.ActionFailed);

            e.EventHandlers.Add<IPropertyAdded>(this.PropertyAdded);
            //e.EventHandlers.Add<IValueChanged<EntityProperty<object>>>(this.PropertyChanged);
            e.EventHandlers.Add<IRestrictedPropertyChange>(this.RestrictedPropertyChange);
        }

        #region EventListeners

        public void ActionStarted(IStarted evt)
        {
            EntityAction<ActionParameters> action = evt.Action as EntityAction<ActionParameters>;
            if (action != null)
            {
                 //ApplicationLogger.Instance().WriteLine(this.Body.Name + " ActionStarted: " + action.Name + " Arguments: " + action.StartArguments);
                if (this.receiverAlive)
                {
                    Send(ACTION_STARTED + " " + action.StartArguments.ToXML());
                }
            }  
        }

        public void ActionSucceeded(ISucceeded evt)
        {
            EntityAction<ActionParameters> action = evt.Action as EntityAction<ActionParameters>;
            if (action != null)
            {
                // DEBUG
                 ApplicationLogger.Instance().WriteLine("-- RemoteCharacter.ActionEnded " + action.Name);

                if (this.receiverAlive)
                {

                    Send(ACTION_FINISHED + " " + action.StartArguments.ToXML());
                }
            }
        }

        public void ActionFailed(IFailed evt)
        {
            EntityAction<ActionParameters> action = evt.Action as EntityAction<ActionParameters>;
            if (action != null)
            {
                ApplicationLogger.Instance().WriteLine("-- RemoteCharacter.ActionFailed " + action.Name);
                if (this.receiverAlive)
                {
                    Send(ACTION_FAILED + " " + action.StartArguments.ToXML());
                }
            }
        }

        public void PropertyAdded(IPropertyAdded evt)
        {
            if (this.receiverAlive)
            {
                Entity entity = evt.Entity;
                IEntityProperty property = evt.Property;
                string msg = PROPERTY_CHANGED + " " + evt.ToXml();
                Send(msg);
            }
        }

        /*public void PropertyChanged(IValueChanged<EntityProperty<object>> evt)
        {
            if (this.receiverAlive)
            {
                Entity entity = evt.Property.Parent;
                string msg = PROPERTY_CHANGED + " * " + entity.Name + " " + evt.Property.Name + " " + evt.NewValue;
                Send(msg);
            }
        }*/

        public void RestrictedPropertyChange(IRestrictedPropertyChange evt)
        {
            if (this.receiverAlive)
            {
             //   Event e = evt as Event;
				//I dont want to send back property changes of emotional states
                if (!evt.Property.Name.Equals("emotionalstate"))
				{
					Entity entity = evt.Entity;
                	string msg = PROPERTY_CHANGED + " " + evt.ToXml();
                	Send(msg);
				}
            }
        }

      
        public void EntityAdded(IAddedTo<Entity,Simulation> evt)
        {
            RegisterEventListeners(evt.Item);
            if (this.receiverAlive)
            {
                string msg = ENTITY_ADDED + " " + evt.Item.Name;
                Send(msg);
            }
        }

        #endregion

    #region RemoteCommunication

        private void ReceiveThread()
        {
            StreamReader socketReader = null;
            string msg = string.Empty;
            try
            {
                while (receiverAlive)
                {
                    if (socketReader == null)
                    {
                        socketReader = new StreamReader(this.socketStream, Encoding.UTF8);
                    }

                    msg = socketReader.ReadLine();
					//TODO: will throw an exception when the package has been fragmented as readline will return null
					Parse(msg);
                }
            }
            catch (Exception e)
            {
                ApplicationLogger.Instance().WriteLine("Agent " + this.Body.Name + " lost the connection with the mind...: " + e.Message);
                ApplicationLogger.Instance().WriteLine(e.StackTrace);
            } 
        }

        public void Send(String msg)
        {
            byte[] aux = Encoding.UTF8.GetBytes(msg + "\n");
            this.socketStream.Write(aux, 0, aux.Length);
            this.socketStream.Flush();
        }

        #endregion
    }
}
