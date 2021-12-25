using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Blazor.Dx;

public partial class CcToast
{
    [Inject] CcToastService ToastSrv { get; set; }

    protected override void OnInitialized()
    {
        ToastSrv.CcToastRef = this;
        base.OnInitialized();
    }

    readonly ConcurrentDictionary<CcToastConfig, CancellationTokenSource> _messages = new ConcurrentDictionary<CcToastConfig, CancellationTokenSource>();

    protected override void OnAfterRender(bool firstRender)
    {
        foreach (var kvp in _messages)
            RemoveMessageAsync(kvp.Key, kvp.Value.Token);
    }

    async void RemoveMessageAsync(CcToastConfig message, CancellationToken ct)
    {
        await Task.Yield();
        try
        {
            await Task.Delay(message.Duration ?? ToastSrv.Duration, ct);
            await InvokeAsync(() =>
            {
                if (_messages.TryRemove(message, out var cts))
                {
                    cts.Dispose();
                    StateHasChanged();
                }
            });
        }
        catch (Exception) { }
    }

    CancellationTokenSource AddValueFactory(CcToastConfig key)
    {
        InvokeAsync(StateHasChanged);
        return new CancellationTokenSource();
    }

    CancellationTokenSource UpdateValueFactory(CcToastConfig key, CancellationTokenSource cts)
    {
        using (cts)
            cts.Cancel();
        cts = new CancellationTokenSource();
        RemoveMessageAsync(key, cts.Token);
        return cts;

    }

    public void OnNext(CcToastConfig msg) => _messages.AddOrUpdate(msg, AddValueFactory, UpdateValueFactory);

    internal void OnClose(CcToastConfig msg)
    {
        if (_messages.TryRemove(msg, out var cts))
        {
            cts.Dispose();
            StateHasChanged();
        }
    }
}


public class CcToastService
{
    public int Duration { get; set; } = 5000;

    internal CcToast CcToastRef { get; set; }

    public void Show(CcToastConfig config)
    {
        CcToastRef.OnNext(config);
    }

    public void Info(string message)
    {
        CcToastRef.OnNext(new CcToastConfig()
        {
            Title = "提示",
            Message = message,
        });
    }

    public void Success(string message)
    {
        CcToastRef.OnNext(new CcToastConfig()
        {
            Title = "成功",
            Message = message,
            IconCssClass = "fas fa-check",
            ToastType = CcToastType.Success,
        });
    }

    public void Error(string message)
    {
        CcToastRef.OnNext(new CcToastConfig()
        {
            Title = "错误",
            Message = message,
            IconCssClass = "fas fa-times",
            ToastType = CcToastType.Error,
        });
    }

    public void Warning(string message)
    {
        CcToastRef.OnNext(new CcToastConfig()
        {
            Title = "警告",
            Message = message,
            IconCssClass = "fas fa-exclamation-triangle",
            ToastType = CcToastType.Warning,
        });
    }

    /// <summary>
    /// 关闭提示
    /// </summary>
    /// <param name="msgConfig"></param>
    void OnClose(CcToastConfig msgConfig)
    {
        CcToastRef.OnClose(msgConfig);
    }

}

public class CcToastConfig
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    public string Subtitle { get; set; }

    public string Message { get; set; }

    public CcToastType ToastType { get; set; } = CcToastType.Info;

    internal string ToastTypeCss => ToastType switch
    {
        CcToastType.Info => "",
        CcToastType.Error => "border-danger",
        CcToastType.Warning => "border-warning",
        CcToastType.Success => "border-success",
        _ => "",
    };

    internal long ShowSort { get; set; } = DateTime.Now.Ticks;

    /// <summary>
    /// 图标
    /// </summary>
    public string IconCssClass { get; set; } = "far fa-comment-alt";

    /// <summary>
    /// 延迟时间
    /// </summary>
    public int? Duration { get; set; }

    /// <summary>
    /// 关闭按钮
    /// </summary>
    public bool ShowCloseButton { get; set; } = true;
}

public enum CcToastType
{
    Success,
    Error,
    Warning,
    Info,
}
