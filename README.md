# CC.Blazor.Dx
基于DevExpress Blazor控件，补充缺失组件（通知、对话框等），再次分装进一步简化组件使用，最终目标是提高开发效率。

Based on the DevExpress Blazor control, the missing components (notifications, dialog boxes, etc.) are supplemented, and the components are repackaged again to further simplify the use of components. The ultimate goal is to improve development efficiency.

# Demo

### MessageBox

![A](https://user-images.githubusercontent.com/7581981/147385543-f421729a-cf61-4533-a80b-1822d6c316f5.png)

``` csharp
[Inject] CcMeesageBoxService MsgSrv { get; set; }

result = await MsgSrv.ShowOkCancelAsync("我是询问");
if (result != CcMeesageBoxResult.Ok) return;
//do something
StateHasChanged();
```

### Toast

![2](https://user-images.githubusercontent.com/7581981/147385546-b7eb0ab4-632f-450c-8016-7c60776b9fdb.png)

``` csharp
[Inject] CcToastService ToastSrv { get; set; }

ToastSrv.Success(DateTime.Now.ToString());
ToastSrv.Warning(DateTime.Now.ToString());
ToastSrv.Error(DateTime.Now.ToString());
ToastSrv.Info(DateTime.Now.ToString());
```
