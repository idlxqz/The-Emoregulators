namespace Assets.Scripts
{
    public class Instruction
    {
        private const int DefaultDelayTime = 3;
        public string Text { get; set; }
        public int DelayTime { get; set; }

        public Instruction(string text, int delay)
        {
            this.Text = text;
            this.DelayTime = delay;
        }

        public Instruction(string text)
        {
            this.Text = text;
            this.DelayTime = Instruction.DefaultDelayTime;
        }
    }
}
