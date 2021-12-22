using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Blazor.Dx
{
    public partial class CcMeesageBox
    {
        [Inject]
        public CcMeesageBoxService MsgSrv { get; set; }

        public bool Visible { get; set; } = false;

        CcMeesageBoxConfig Config;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            MsgSrv.ShowEvent = ShowEvent;
        }


        void ShowEvent(CcMeesageBoxConfig e)
        {
            Config = e;
            Visible = true;
            InvokeAsync(StateHasChanged);
        }


        void OnOkClick()
        {
            Visible = false;
            Config?.Callback?.Invoke(CcMeesageBoxResult.Ok);
            MsgSrv.TaskCompletionSource.SetResult(CcMeesageBoxResult.Ok);
        }

        void OnCancelClick()
        {
            Visible = false;

            Config?.Callback?.Invoke(CcMeesageBoxResult.Cancel);
            MsgSrv.TaskCompletionSource.SetResult(CcMeesageBoxResult.Cancel);
        }
    }


}

public class CcMeesageBoxService
{
    internal Action<CcMeesageBoxConfig> ShowEvent;

    public TaskCompletionSource<CcMeesageBoxResult> TaskCompletionSource { get; set; }

    public async Task<CcMeesageBoxResult> ShowAsync(CcMeesageBoxConfig config)
    {
        ShowEvent?.Invoke(config);
        TaskCompletionSource = new TaskCompletionSource<CcMeesageBoxResult>();
        return await TaskCompletionSource.Task;
    }

    /// <summary>
    /// 提示框
    /// </summary>
    /// <param name="text"></param>
    /// <param name="caption"></param>
    /// <param name="callback"></param>
    public async Task<CcMeesageBoxResult> ShowOkAsync(string content, string caption = "提示")
    {
        return await ShowAsync(new CcMeesageBoxConfig()
        {
            Content = content,
            Caption = caption,
            Buttons=CcMeesageBoxButtons.Ok,
        });
    }

    /// <summary>
    /// 是和取消提示框
    /// </summary>
    /// <param name="text"></param>
    /// <param name="caption"></param>
    /// <param name="callback"></param>
    public async Task<CcMeesageBoxResult> ShowOkCancelAsync(string content, string caption = "提示")
    {
        return await ShowAsync(new CcMeesageBoxConfig()
        {
            Content = content,
            Caption = caption,
            Buttons = CcMeesageBoxButtons.OkCancel,
        });
    }
}

public class CcMeesageBoxConfig
{
    public string Caption { get; set; }

    public string Content { get; set; }

    public string Footer { get; set; }

    public CcMeesageBoxButtons Buttons { get; set; }

    /// <summary>
    /// 点击按钮后回调
    /// </summary>
    public Action<CcMeesageBoxResult> Callback { get; set; }
}


public enum CcMeesageBoxResult
{
    Ok,
    Cancel,
}

public enum CcMeesageBoxButtons
{
    OkCancel,
    Ok,
}

/*
var result = await MsgSrv.ShowAsync(new CcMeesageBoxConfig()
{
    Caption="标题",
    Content="内容",
    Buttons=CcMeesageBoxButtons.OkCancel
});
 StateHasChanged();
*/