using System;

namespace DfViewer
{
	public class DfStatus
	{
		public string FilesystemPath {
			get;
			private set;
		}

		public long DfSize {
			get;
			private set;
		}

		public long DfUsed {
			get;
			private set;
		}

		public long DfFree {
			get;
			private set;
		}


		public DfStatus(string filesystemPath, long dfSize, long dfUsed, long dfFree)
		{
			this.FilesystemPath = filesystemPath;
			this.DfSize = dfSize;
			this.DfUsed = dfUsed;
			this.DfFree = dfFree;
		}
	}
}
