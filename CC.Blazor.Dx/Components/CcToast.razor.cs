using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Blazor.Dx
{
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
        public void OnError(Exception e) => throw e;
        public void OnCompleted() => throw new NotImplementedException();

        void OnClose(CcToastConfig msg)
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
        public int Duration { get; set; } = 3000;

        internal CcToast CcToastRef { get; set; }

        public void Info(string message)
        {
            CcToastRef.OnNext(new CcToastConfig()
            {
                Title = "消息",
                Message = message,
            });
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


}
