using System.Threading;
using System.Windows;
using System.Windows.Threading;
using MultipleThreadsApp.Context;
using MultipleThreadsApp.Models;
using MultipleThreadsApp.ViewModels;

namespace MultipleThreadsApp
{
	public partial class MainWindow : Window
	{
		private readonly TestViewModel _vm;
		private double _progressBarGradient;
		private bool _isWritingCanceled;

		public MainWindow()
		{
			InitializeComponent();
			_vm = new TestViewModel();
			this.DataContext = _vm;
		}

		private void Button_OnGoClick(object sender, RoutedEventArgs e)
		{
			cancelButton.IsEnabled = true;
			_isWritingCanceled = false;
			_progressBarGradient = (double)100 / (_vm.Threads * _vm.Rows);

			var length = _vm.Threads;
			for (var i = 0; i < length; i++)
			{
				var threadWriting = new Thread(WriteTestRow);
				threadWriting.Start();
			}
		}

		private void Button_OnStopClick(object sender, RoutedEventArgs e)
		{
			_isWritingCanceled = true;
			ResetValues();
		}

		private void WriteTestRow()
		{
			using (var context = new TestContext())
			{
				var length = _vm.Rows;
				for (var i = 0; i < length; i++)
				{
					if (_isWritingCanceled)
					{
						break;
					}
					this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
					{
						var progressBarCurrentValue = progressBar.Value;
						progressBar.Value = progressBarCurrentValue + _progressBarGradient;
					});

					context.TestModels.Add(new TestModel { TestRow = 1 });
					context.SaveChanges();
				}
			}

			ResetValues();
		}

		private void ResetValues()
		{
			this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
		   {
			   progressBar.Value = 0;
			   cancelButton.IsEnabled = false;
		   });
			_progressBarGradient = 0;
		}
	}
}