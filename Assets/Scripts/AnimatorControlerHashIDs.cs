using UnityEngine;

public class AnimatorControlerHashIDs : MonoBehaviour
{

    public int IdleState;
    public int BreathingExerciseState;
    public int DancingState;
    public int SqueezingBallState;
    public int BreathingExerciseTrigger;

    public void Awake()
    {
        this.IdleState = Animator.StringToHash("Base Layer.idle_stationary");
        this.BreathingExerciseState = Animator.StringToHash("Base Layer.breathing_exercise");
        this.DancingState = Animator.StringToHash("Base Layer.dancing");
        this.SqueezingBallState = Animator.StringToHash("Base Layer.squeezing_ball_with_one_hand");
        this.BreathingExerciseTrigger = Animator.StringToHash("BreathingExercise");
    }

}
