using AdbShell.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WSA_AdbShell.Models;

namespace WSA_ADBShell.ViewModels.ItemsViewModels;


[INotifyPropertyChanged]
public partial class TaskManagerItemVM: TaskManagerData
{
	public TaskManagerItemVM()
	{
        this.OutputChanged += TaskManagerItemVM_OutputChanged;
        this.Exited += TaskManagerItemVM_Exited;
		this.IsRun = true;
	}

    private void TaskManagerItemVM_Exited(object sender, EventArgs e)
    {
		IsRun = true;
    }

    private void TaskManagerItemVM_OutputChanged(WSA_AdbShell.Events.TaskManagerEvent taskManagerEvent)
    {
		this.NowOutput = taskManagerEvent.Arg.Output;

    }

    [RelayCommand]
	void Start()
	{
		this.StartProcess();
		IsRun = false;
	}

	[ObservableProperty]
	bool _isRun;


    partial void OnIsRunChanged(bool value)
    {
		switch (value)
		{
			case true:
				this.StringState = "启动";
				break;
			case false:
				this.StringState = "正在运行";
				break;
		}
	}

    [ObservableProperty]
	string _NowOutput;

	[ObservableProperty]
	string _stringState;
}
