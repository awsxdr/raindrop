namespace Raindrop.Domain.WriteModel.AggregateRoots
{
    using System;

    using CQRSlite.Domain;

    using Objects;

    using ReadModel.Events;

    using Utility;

    public class Side : AggregateRoot
    {
        public Guid SideId { get; } = Guid.NewGuid();
        public Team Team { get; }
        public int Score { get; }
        public int TimeoutsRemaining { get; }
        public OfficialReviewStatus OfficialReviewStatus { get; }

        public Side(
            Team team,
            int score,
            int timeoutsRemaining,
            OfficialReviewStatus officialReviewStatus)
        {
            Team = team;
            Score = score;
            TimeoutsRemaining = timeoutsRemaining;
            OfficialReviewStatus = officialReviewStatus;
        }

        public Side() { }

        private Side(
            Guid sideId,
            Team team,
            int score,
            int timeoutsRemaining,
            OfficialReviewStatus officialReviewStatus)
            : this(team, score, timeoutsRemaining, officialReviewStatus)
        {
            SideId = sideId;
        }

        public Side SetScore(int score) =>
            CloneWithChanges(score: score)
            .Tee(x => ApplyChange(new SideScoreChangedEvent(SideId, score, Version)));

        private Side CloneWithChanges(
            Team team = null,
            int? score = null,
            int? timeoutsRemaining = null,
            OfficialReviewStatus? officialReviewStatus = null)
            =>
            new Side(
                SideId,
                team ?? Team,
                score ?? Score,
                timeoutsRemaining ?? TimeoutsRemaining,
                officialReviewStatus ?? OfficialReviewStatus);
    }
}
