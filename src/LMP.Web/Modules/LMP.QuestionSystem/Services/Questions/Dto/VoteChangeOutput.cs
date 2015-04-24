namespace LMP.QuestionSystem.Services.Dtos
{
    public class VoteChangeOutput
    {
        public int VoteCount { get; set; }

        public VoteChangeOutput()
        {
            
        }

        public VoteChangeOutput(int voteCount)
        {
            VoteCount = voteCount;
        }
    }
}