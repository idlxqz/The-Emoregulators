using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ION.Meta;

namespace ION.Core.Extensions
{
	public enum AnimationActionType{
			Default,
			StartEndlessAnimation,
			StopEndlessAnimation,
			NoAnimation,
	}
	
    public class EntityAnimationAction<T> : EntityAction<T>
    {
		public bool EndlessAnimation { get; private set; }
        public Entity Parent { get; private set; }
        public string Name { get; private set; }
		
		List<string> affectedBodyParts = new List<string>(3);
		
		public List<string> AffectedBodyparts {get{return affectedBodyParts;}}
		
		
		
		AnimationActionType animationType = AnimationActionType.Default;
		public AnimationActionType Type {get{return animationType;} set{animationType = value; }}
		
		bool overridesEmotions = false;
		public bool OverridesEmotions{get{return overridesEmotions;}}
		
        public EntityAnimationAction(Entity owner, bool endlessAnimation, string name, AnimationActionType _type, bool _overridesEmotions = false)
            : base(owner,name)
        {
			this.EndlessAnimation = endlessAnimation;
			this.overridesEmotions = _overridesEmotions;
			this.animationType = _type;
        }
		
		public void affectBodyPart(string _partName){
			if(!affectedBodyParts.Contains(_partName)){
				affectedBodyParts.Add(_partName);
			}
		}
		
		public void removeAffectedBodyPart(string _partName){
			if(affectedBodyParts.Contains(_partName)){
				affectedBodyParts.Remove(_partName);
			}
		}
		
		
    }
}
