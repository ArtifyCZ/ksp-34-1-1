using System;
using System.Collections.Generic;

namespace KSP_34_H1_1
{
    public class Point : IComparable
    {
        public uint Id { get; }

        public PointType Type { get; }

        public ulong Time { get; set; }

        public IEnumerable<Neighborhood> Neighbors => this._neighbors;

        private ICollection<Neighborhood> _neighbors { get; }

        public Point(uint id, PointType type)
        {
            this.Id = id;
            this.Type = type;
            this._neighbors = new List<Neighborhood>();
        }

        public void AddNeighbor(Neighborhood neighborhood) => this._neighbors.Add(neighborhood);

        public enum PointType
        {
            Ski,
            Walk
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
