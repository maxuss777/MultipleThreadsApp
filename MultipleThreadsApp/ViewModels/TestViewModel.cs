using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MultipleThreadsApp.Annotations;

namespace MultipleThreadsApp.ViewModels
{
	public class TestViewModel : IDataErrorInfo, INotifyPropertyChanged
	{
		private int _rows;
		public int Rows
		{
			get { return _rows; }
			set
			{
				_rows = value;
				OnPropertyChanged();
			}
		}

		private int _threads;
		public int Threads
		{
			get { return _threads; }
			set
			{
				_threads = value;
				OnPropertyChanged();
			}
		}

		public string this[string columnName]
		{
			get
			{
				string error = string.Empty;
				switch (columnName)
				{
					case "Rows":
						if (Rows <= 0 || Rows > 1000)
						{
							error = "Such rows amount are not allowed";
						}
						break;
					case "Threads":
						if (Threads <= 0 || Threads > 100)
						{
							error = "Such threads amount are not allowed";
						}
						break;
				}

				return error;
			}
		}

		public string Error
		{
			get { throw new NotImplementedException(); }
		}

		public event PropertyChangedEventHandler PropertyChanged;
		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}