using UnityEngine;

public class AnimatorControlerHashIDs : MonoBehaviour
{

    public int IdleState;
    public int BreathingExerciseState;
    public int BreathingExerciseTrigger;
    public int DancingExerciseState;
    public int DancingExerciseTrigger;
    public int DancingExerciseBool;
    public int SqueezingExerciseState;
    public int SqueezingExerciseTrigger;
    public int StreachingExerciseState;
    public int StreachingExerciseTrigger;
    public int SnailExerciseState;
    public int SnailExerciseTrigger;
    public int SandExerciseState;
    public int SandExerciseTrigger;


    public void Awake()
    {
        this.IdleState = Animator.StringToHash("Base Layer.Idle");
        this.BreathingExerciseState = Animator.StringToHash("Base Layer.BreathingExercise");
        this.BreathingExerciseTrigger = Animator.StringToHash("BreathingExercise");
        this.DancingExerciseState = Animator.StringToHash("Base Layer.DancingExercise");
        this.DancingExerciseTrigger = Animator.StringToHash("DancingExerciseTri");
        this.DancingExerciseBool = Animator.StringToHash("DancingExercise");
        this.SqueezingExerciseState = Animator.StringToHash("Base Layer.SqueezingExercise");
        this.SqueezingExerciseTrigger = Animator.StringToHash("SqueezingExercise");
        this.StreachingExerciseState = Animator.StringToHash("Base Layer.StreachingExercise");
        this.StreachingExerciseTrigger = Animator.StringToHash("StreachingExercise");
        this.SnailExerciseState = Animator.StringToHash("Base Layer.SnailExercise");
        this.SnailExerciseTrigger = Animator.StringToHash("SnailExercise");
        this.SandExerciseState = Animator.StringToHash("Base Layer.SandExercise");
        this.SandExerciseTrigger = Animator.StringToHash("SandExercise");
    }

}
