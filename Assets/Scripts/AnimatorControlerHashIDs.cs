using UnityEngine;

public class AnimatorControlerHashIDs 
{
    #region singleton pattern
    private static AnimatorControlerHashIDs instance = null;

    public static AnimatorControlerHashIDs Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AnimatorControlerHashIDs();
            }
            return instance;
        }
    }

    #endregion
    public int IdleState { get; private set; }
    public int BreathingExerciseState { get; private set; }
    public int BreathingExerciseTrigger { get; private set; }
    public int DancingExerciseState { get; private set; }
    public int DancingExerciseTrigger { get; private set; }
    public int DancingExerciseBool { get; private set; }
    public int SqueezingExerciseState { get; private set; }
    public int SqueezingExerciseTrigger { get; private set; }
    public int StreachingExerciseState { get; private set; }
    public int StreachingExerciseTrigger { get; private set; }
    public int SnailExerciseState { get; private set; }
    public int SnailExerciseTrigger { get; private set; }
    public int SandExerciseState { get; private set; }
    public int SandExerciseTrigger { get; private set; }

    private AnimatorControlerHashIDs()
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
