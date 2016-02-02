using System.Collections.Generic;

namespace TeamCitySharper.DomainEntities
{
	public class SnapshotDependencies
	{
		public override string ToString()
		{
			return "snapshot-dependencies";
		}

		public List<SnapshotDependency> SnapshotDependency { get; set; }
	}
}