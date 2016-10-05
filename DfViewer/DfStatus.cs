using System;
using System.Collections.Generic;
using System.IO;

namespace DfViewer
{
	/// <summary>
	/// Makes available <c>df</c> ("disk free") status.
	/// <remarks>All sizes in MiB.</remarks>
	/// </summary>
	public class DfStatus
	{
		/// <summary>
		/// Gets the filesystem path associated with this <c>df</c> status.
		/// </summary>
		/// <value>The filesystem path.</value>
		public string FilesystemPath {
			get;
			private set;
		}

		/// <summary>
		/// Gets the disk size associated with this <c>df</c> status.
		/// <remarks>All sizes in MiB.</remarks>
		/// </summary>
		/// <value>The disk size.</value>
		public long DfSize {
			get;
			private set;
		}

		/// <summary>
		/// Gets the used disk space associated with this <c>df</c> status.
		/// <remarks>All sizes in MiB.</remarks>
		/// </summary>
		/// <value>The used disk space.</value>
		public long DfUsed {
			get;
			private set;
		}

		/// <summary>
		/// Gets the free disk space associated with this <c>df</c> status.
		/// <remarks>All sizes in MiB.</remarks>
		/// </summary>
		/// <value>The free disk space.</value>
		public long DfFree {
			get;
			private set;
		}


		/// <summary>
		/// Constructs a <see cref="DfStatus"/> instance from previously available data.
		/// </summary>
		/// <param name="filesystemPath">Filesystem path.</param>
		/// <param name="dfSize">Disk size, in MiB.</param>
		/// <param name="dfUsed">Used disk space, in MiB.</param>
		/// <param name="dfFree">Free disk space, in MiB.</param>
		public DfStatus(string filesystemPath, long dfSize, long dfUsed, long dfFree)
		{
			this.FilesystemPath = filesystemPath;
			this.DfSize = dfSize;
			this.DfUsed = dfUsed;
			this.DfFree = dfFree;
		}

		/// <summary>
		/// Constructs a <see cref="DfStatus"/> instance from a filesystem path.
		/// The <c>df</c> status is then acquired via the Operating System.
		/// In this test/proof-of-concept code I don't actually call <c>df</c>,
		/// but use the framework to produce values that are hopefully equivalent.
		/// </summary>
		/// <param name="filesystemPath">Filesystem path.</param>
		public DfStatus(string filesystemPath)
		{
			if (object.ReferenceEquals(filesystemPath, null))
				throw new ArgumentNullException("filesystemPath");

			this.FilesystemPath = filesystemPath;

			var info = new DriveInfo(this.FilesystemPath);
			this.DfSize = info.TotalSize / (1024 * 1024);
			this.DfFree = info.AvailableFreeSpace / (1024 * 1024);
			this.DfUsed = this.DfSize - info.TotalFreeSpace / (1024 * 1024);
		}


		/// <summary>
		/// Creates <see cref="DfStatus"/> instances for all known/mounted filesystems.
		/// (I hope.)
		/// </summary>
		/// <returns>The df statuses.</returns>
		public static IList<DfStatus> GetDfStatuses()
		{
			var ret = new List<DfStatus>();

			foreach (DriveInfo info in DriveInfo.GetDrives()) {
				ret.Add(new DfStatus(info.Name));
			}

			return ret;
		}
	}
}
