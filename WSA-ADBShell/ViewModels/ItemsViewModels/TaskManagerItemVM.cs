using AdbShell;
using AdbShell.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WSA_AdbShell.Models;
using WSA_ADBShell.Events.ViewModelsEvent;
using WSA_ADBShell.Services.Interfaces;

namespace WSA_ADBShell.ViewModels.ItemsViewModels;


[INotifyPropertyChanged]
public partial class TaskManagerItemVM: TaskManagerData,ITaskToken
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

	/// <summary>
	/// 初始化Token
	/// </summary>
	public void Init(ITaskManager taskManager)
	{
		Token = taskManager.CraeteToken(this);
		this.TaskManager = taskManager;
	}


    [RelayCommand]
    void RemoveMe()
    {
		var result =  TaskManager.RemoveToken(this);
		if (result)
		{
			WeakReferenceMessenger.Default.Send<TokenEvent>(new TokenEvent() { RemoveTask = this});
		}
    }


    public string Token { get; set; }

    [RelayCommand]
	void Start()
	{
		if (this.Token == null || this.TaskManager == null) throw new Exception("未使用Init方法初始化该任务");
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

	public string TokenSource { get => this.TaskType.ToString(); }

    public string TokenName => this.Title;

}
