using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Diagnostics;
using ION.Core;
using ION.Core.Extensions;

namespace FAtiMA.RemoteAgent
{
    public class LanguageEngineMaster
    {
        public const string AGENT_LANGUAGE = "AgentLanguageEngine";
        public const string USER_LANGUAGE = "UserLanguageEngine";
        public const string LANGUAGE_ENGINE = "LanguageEngine";
        public const string AGENT_SEX = "AgentSex";
        public const string USER_SEX = "UserSex";

        public const string SAY_REQUEST = "Say";
        public const string INPUT_REQUEST = "Input";
        public const string NARRATE_REQUEST = "Narrate";
        public const string KILL_REQUEST = "Kill";

        public const int PORT = 45500;

        private Socket master;
        private Socket slave;

        private string agentSex;
        private string userSex;
        private string agentLanguageFile;
        private string userLanguageFile;

        public LanguageEngineMaster(string agentSex, string userSex, string agentLanguageFile, string userLanguageFile)
        {
            this.master = null;
            this.slave = null;
            this.agentSex = agentSex;
            this.userSex = userSex;
            this.agentLanguageFile = agentLanguageFile;
            this.userLanguageFile = userLanguageFile;
        }

        ~LanguageEngineMaster()
        {
            ApplicationLogger.Instance().WriteLine("DESTROYYYYYY!!!!!!!!!!!!");
            this.Close();
        }

        public string AgentSex
        {
            get
            {
                return this.agentSex;
            }
        }

        public string UserSex
        {
            get
            {
                return this.userSex;
            }
        }

        public string AgentLanguageFile
        {
            get
            {
                return this.agentLanguageFile;
            }
        }

        public string UserLanguageFile
        {
            get
            {
                return this.userLanguageFile;
            }
        }

        public bool IsReady
        {
            get
            {
                return this.slave != null;
            }
        }

        private int StartMaster()
        {
            // Establish local endpoint...
            IPAddress ipAddress = IPAddress.Any;            
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress,PORT);
            
            // Create a TCP/IP socket...
            this.master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Bind the socket...
            this.master.Bind(localEndPoint);

            this.master.Listen(5);
            
            return localEndPoint.Port;
        }

        private void ConnectToSlave()
        {
            //this.master.Blocking = false;
            try
            {
                this.slave = this.master.Accept();
                this.slave.Blocking = true;
            }
            catch (SocketException s)
            {
                Console.WriteLine(s.StackTrace);
            }

            this.master.Blocking = true;
        }

        public string Say(SpeechActParameters speech)
        {
            string utterance = null;
            string[] aux;
            string result = processLanguageRequest(SAY_REQUEST, speech.toLanguageEngine());

            if (result != null)
            {
                string[] delimiters = new string[2];
                delimiters[0] = "<Utterance>";
                delimiters[1] = "</Utterance";
                aux = result.Split(delimiters,StringSplitOptions.None);
                if (aux.GetUpperBound(0) > 0)
                {
                    utterance = aux[1];
                }
                else
                {
                    utterance = "...";
                }
                //utterance = result.Split("<Utterance>".ToCharArray())[1].Split("</Utterance".ToCharArray())[0];
            }
            else
            {
                utterance = "...";
            }

            return utterance;
        }

        public SpeechActParameters Input(string input)
        {
            SpeechActParameters newSpeech;
            string result = processLanguageRequest(INPUT_REQUEST, input);

            if (result != null)
            {
                newSpeech = (SpeechActParameters)SpeechActParser.Instance.Parse(result);
                return newSpeech;
            }
            else
            {
                return null;
            }
        }

        public string Narrate(String amSummary)
        {
            string result = processLanguageRequest(NARRATE_REQUEST, amSummary);

            if (result != null)
            {
                string[] delimiters = new string[2];
                delimiters[0] = "<Summary>";
                delimiters[1] = "</Summary";
                string[] aux2 = result.Split(delimiters,StringSplitOptions.None);
                if (aux2.Length > 1)
                {
                    return aux2[1];
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        private string processLanguageRequest(string method, string speechAct)
        {
            string answer = null;
            try
            {
                ApplicationLogger.Instance().WriteLine("Processing Summary Request: " + speechAct);

                Send(method + " " + speechAct);

                NetworkStream socketStream = new NetworkStream(this.slave);
                //receiving answer to request

                StreamReader reader = new StreamReader(socketStream,UTF8Encoding.UTF8);
                answer = reader.ReadLine();
                System.Console.WriteLine(answer);
            }
            catch (Exception e)
            {
                throw new Exception("The application cannot continue. A critical error with the LanguageServer occurred: " + e.Message);
            }

            return answer;
        }

        protected void OnDestroy()
        {
            this.Close();
        }

        private void Send(String message)
        {
            //sending request
            NetworkStream socketStream = new NetworkStream(this.slave);
            
            byte[] aux = Encoding.UTF8.GetBytes(message + "\n");
            //byte[] aux = ASCIIEncoding.ASCII.GetBytes(message + "\n");
            socketStream.Write(aux, 0, aux.Length);
            socketStream.Flush();
        }

        public void Close()
        {
            if (this.slave != null)
            {
                Send(KILL_REQUEST);
                this.slave.Close();
                this.slave = null;
            }
            if (this.master != null)
            {
                this.master.Close();
                this.master = null;
            }
            
        }

        public void Load()
        {

            int connectionPort = this.StartMaster();
            //once the Server is on, we can launch the java proccess that corresponds to the slave

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
		
			OperatingSystem osInfo = Environment.OSVersion;
			
			if(osInfo.Platform == PlatformID.Unix){ 
				LaunchUnix(connectionPort, proc);
			}
			else if(osInfo.Platform == PlatformID.MacOSX){
				LaunchUnix(connectionPort, proc);
			}
			else {
				LaunchWindows(connectionPort, proc);
			}

            this.ConnectToSlave();
        }
		
		
		
		// Henrique Campos - made this Agent launcher for Windows 
		private void LaunchWindows(int port, Process proc){
			// For Windows:
			ApplicationLogger.Instance().WriteLine("Launching LanguageEngine for Windows..");
	        proc.StartInfo.FileName ="cmd";
	        proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
	        proc.StartInfo.UseShellExecute = true;
			
			//port, agentSex, userSex, agentLanguage, userLanguage
            proc.StartInfo.Arguments = "/k java -cp \"xercesImpl.jar;spin.jar;LanguageEngine.jar;LanguageServer.jar\" " +
                "LanguageServerSlave " +
                port + " " +
                this.agentSex + " " +
                this.userSex + " " +
                this.agentLanguageFile + " " +
                this.userLanguageFile;
		
			proc.Start();
		}
		
		// Henrique Campos - made this Agent launcher for Unix and Mac 
		private void LaunchUnix(int port, Process proc){
			// For Mac:
			
			ApplicationLogger.Instance().WriteLine("Launching LanguageEngine for Unix..");
			proc.StartInfo.FileName = "java";
			//proc.StartInfo.Arguments = "/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
			proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
			//proc.StartInfo.RedirectStandardInput = true;
			proc.StartInfo.CreateNoWindow = true;
			proc.StartInfo.UseShellExecute = true;
			//proc.StartInfo.RedirectStandardOutput = true;
			//proc.StartInfo.RedirectStandardInput = true;
			//proc.StartInfo.RedirectStandardError = true;
			
			//port, agentSex, userSex, agentLanguage, userLanguage
            proc.StartInfo.Arguments = "-cp \"xercesImpl.jar;spin.jar;LanguageEngine.jar;LanguageServer.jar\" " +
                "LanguageServerSlave " +
                port + " " +
                this.agentSex + " " +
                this.userSex + " " +
                this.agentLanguageFile + " " +
                this.userLanguageFile;
			
			proc.Start ();
		}
	}
}
