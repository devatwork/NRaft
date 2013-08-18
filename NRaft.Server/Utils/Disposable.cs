using System;
using System.Threading;

namespace NRaft.Server.Utils
{
	/// <summary>
	/// DisposableBase class. Represents an implementation of the IDisposable interface.
	/// </summary>
	/// <remarks>http://blogs.msdn.com/b/blambert/archive/2009/07/24/a-simple-and-totally-thread-safe-implementation-of-idisposable.aspx</remarks>
	public abstract class Disposable : IDisposable
	{
		#region Constructors / Destructors
		/// <summary>
		/// Finalizes an instance of the DisposableBase class.
		/// </summary>
		~Disposable()
		{
			// The destructor has been called as a result of finalization, indicating that the object
			// was not disposed of using the Dispose() method. In this case, call the DisposeResources
			// method with the disposeManagedResources flag set to false, indicating that derived classes
			// may only release unmanaged resources.
			DisposeResources(false);
		}
		#endregion
		#region IDisposable Members
		/// <summary>
		/// Performs application-defined tasks associated with disposing of resources.
		/// </summary>
		public void Dispose()
		{
			// Attempt to move the disposable state from 0 to 1. If successful, we can be assured that
			// this thread is the first thread to do so, and can safely dispose of the object.
			if (Interlocked.CompareExchange(ref disposableState, 1, 0) != 0)
				return;

			// Call the DisposeResources method with the disposeManagedResources flag set to true, indicating
			// that derived classes may release unmanaged resources and dispose of managed resources.
			DisposeResources(true);

			// Suppress finalization of this object (remove it from the finalization queue and
			// prevent the destructor from being called).
			GC.SuppressFinalize(this);
		}
		#endregion
		#region Template Methods
		/// <summary>
		/// Dispose resources. Override this method in derived classes. Unmanaged resources should always be released
		/// when this method is called. Managed resources may only be disposed of if disposeManagedResources is true.
		/// </summary>
		/// <param name="disposeManagedResources">A value which indicates whether managed resources may be disposed of.</param>
		protected abstract void DisposeResources(bool disposeManagedResources);
		#endregion
		#region Helper Methods
		/// <summary>
		/// Checks whether this objects is already disposed.
		/// </summary>
		/// <exception cref="ObjectDisposedException">Thrown when the object is disposed.</exception>
		protected void CheckDisposed()
		{
			if (IsDisposed)
				throw new ObjectDisposedException(string.Format("{0} is disposed.", GetType().Name));
		}
		#endregion
		#region Properties
		/// <summary>
		/// Gets a value indicating whether the object is disposed.
		/// </summary>
		protected bool IsDisposed
		{
			get { return Thread.VolatileRead(ref disposableState) == 1; }
		}
		#endregion
		#region Private Fields
		/// <summary>
		/// A value which indicates the disposable state. 0 indicates undisposed, 1 indicates disposing or disposed.
		/// </summary>
		private int disposableState;
		#endregion
	}
}