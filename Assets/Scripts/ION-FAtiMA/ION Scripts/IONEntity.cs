using System.Linq;
using System.Collections.Generic;
using ION.Meta;
using ION.Core;
using ION.SyncCollections;

using ION.Core.Extensions;

using UnityEngine;

using System.Text;


public class IONEntity : MonoBehaviour
{
	/// <summary>
	/// The name of the entity.
	/// </summary>
	public string entityName;
	/// <summary>
	/// The ION entity associated with this component
	/// </summary>
	protected Entity entity;
	/// <summary>
	/// has this entity been initialized yet?
	/// </summary>
	private bool initialized = false;
	
	
	public bool Initialized {
		get
		{
			return this.initialized;
		}
	}

	public Entity Entity
	{
		get
		{
			return this.entity;
		}
	}
	
	
	/// <summary>
	/// use this for initializing class properties
	/// </summary>
	protected virtual void Awake()
	{
		this.entity = new Entity(entityName);
	}
	
	/// <summary>
	/// Christopher: moved initialisation code to start() function
	/// </summary>
	protected virtual void Start()
	{	
		//initialization
		if(!this.initialized)
		{			
			StringBuilder debugText = new StringBuilder(); //better to do it this way, as it produces less Debug statements
			Debug.Log("<IONEntity> Start initialization for: " + this.entity.Name);	
			
			Component[] actionComponents = this.GetComponents (typeof(ICharacterAction));
			foreach (ICharacterAction actionComponent in actionComponents)
			{
				actionComponent.Initialize(); // Henrique Campos - added this regarding new initialization process
				
				//Debug.Log("<IONEntity> Action " + actionComponent.Action.Name + " added to Entity " + this.entity.Name);
				debugText.AppendLine("<IONEntity> Action " + actionComponent.Action.Name + " added to Entity " + this.entity.Name);
				this.entity.AddAction(actionComponent.Action);
			}
			Component[] propertyComponents = this.GetComponents(typeof(IIONProperty));
			foreach (IIONProperty property in propertyComponents)
			{
				property.Initialize(); // Henrique Campos - added this regarding new initialization process
				
				//Debug.Log("<IONEntity> Property " + property.IONProperty.Name + " added to Entity " + this.entity.Name);
				debugText.AppendLine("<IONEntity> Property " + property.IONProperty.Name + " added to Entity " + this.entity.Name);
				this.entity.AddProperty(property.IONProperty); 
			}				
			
			this.entity.AddToSimulation(Simulation.Instance);	
			
			Debug.Log("<IONEntity> Initialization completed for: " + this.entity.Name + "\n" + debugText.ToString());
			this.initialized = true;
		}
	}	
	
	/// <summary>
	/// called by Unity3d when MonoBehaviour is being destroyed
	/// </summary>
	public virtual void OnDestroy(/*void*/){
		
		entity.RemoveFromSimulation();
		entity = null;
		Debug.Log("<IONEntity> Removed entity: " + this.entityName + " from simulation");
	}/*END OF OnDestroy(void)*/
	
	/// <summary>
	/// Determines whether this instance has property the specified name.
	/// </summary>
	public bool HasProperty(string name)
	{
		return this.Entity.getPropertyByName(name) != null;
	}
	
	/// <summary>
	/// Gets the property value.
	/// </summary>
	/// <returns>
 	public T GetPropertyValue<T>(string name)
 	{
		EntityProperty<T> p = this.Entity.getPropertyByName(name) as EntityProperty<T>;
		if(p!=null)
		{
			return p.Value;
		}
		else throw new PropertyUnavailableException(this.name,name);
 	}
	
	/// <summary>
	/// Sets the property value.
	/// </summary>
    public void SetPropertyValue<T>(string name, T value)
    {
		this.Entity.SetProperty<T>("*",name,value);
       
    }
	
	public void SetPropertyValue<T>(string visibility, string name,T value)
	{
		this.Entity.SetProperty<T>(visibility,name,value);
	}
	
	/// <summary>
	/// Property unavailable exception.
	/// </summary>
	protected class PropertyUnavailableException : UnityException
	{
		public PropertyUnavailableException(string entity, string property)
		: base("<IONEntity> Property " + property + " in Entity " + entity + " is not available") {}
	}
	
}