#pragma checksum "/Users/esmerlinmieses/Personal Folder/C# Projects/BlazingPizza/BlazingPizza.ComponentsLibrary/Authentication/UserStateProvider.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f52174d65e71742e9041ab6ec4cc21c9c3cb4c6d"
// <auto-generated/>
#pragma warning disable 1591
namespace BlazingPizza.ComponentsLibrary.Authentication
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;
    using System.Net.Http;
    public class UserStateProvider : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.RenderTree.RenderTreeBuilder builder)
        {
            __Blazor.BlazingPizza.ComponentsLibrary.Authentication.UserStateProvider.TypeInference.CreateCascadingValue_0(builder, 0, 1, this, 2, (builder2) => {
                builder2.AddMarkupContent(3, "\n    ");
                builder2.AddContent(4, ChildContent);
                builder2.AddMarkupContent(5, "\n");
            }
            );
        }
        #pragma warning restore 1998
#line 10 "/Users/esmerlinmieses/Personal Folder/C# Projects/BlazingPizza/BlazingPizza.ComponentsLibrary/Authentication/UserStateProvider.razor"
            
    private List<TaskCompletionSource<bool>> pendingSignInFlows = new List<TaskCompletionSource<bool>>();

    [Parameter] RenderFragment ChildContent { get; set; }

    public UserState CurrentUser { get; private set; }

    public bool IsLoggedIn => CurrentUser?.IsLoggedIn ?? false;

    protected override async Task OnInitAsync()
    {
        CurrentUser = await HttpClient.GetJsonAsync<UserState>("user");
    }

    public async Task SignIn()
    {
        await JSRuntime.InvokeAsync<object>("openLoginPopup", new DotNetObjectRef(this));
    }

    public async Task SignOut()
    {
        // Transition to "loading" state synchronously, then asynchronously update
        CurrentUser = null;
        StateHasChanged();

        CurrentUser = await HttpClient.PutJsonAsync<UserState>("user/signout", null);
        StateHasChanged();
    }

    public Task<bool> TrySignInAsync()
    {
        if (IsLoggedIn)
        {
            return Task.FromResult(true);
        }

        var resultTcs = new TaskCompletionSource<bool>();
        pendingSignInFlows.Add(resultTcs);
        _ = SignIn();
        return resultTcs.Task;
    }

    [JSInvokable]
    public void OnSignInStateChanged(UserState newUserState)
    {
        CurrentUser = newUserState;
        StateHasChanged();

        foreach (var tcs in pendingSignInFlows)
        {
            tcs.SetResult(newUserState.IsLoggedIn);
        }
        pendingSignInFlows.Clear();
    }

#line default
#line hidden
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient HttpClient { get; set; }
    }
}
namespace __Blazor.BlazingPizza.ComponentsLibrary.Authentication.UserStateProvider
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateCascadingValue_0<T>(global::Microsoft.AspNetCore.Components.RenderTree.RenderTreeBuilder builder, int seq, int __seq0, T __arg0, int __seq1, global::Microsoft.AspNetCore.Components.RenderFragment __arg1)
        {
        builder.OpenComponent<global::Microsoft.AspNetCore.Components.CascadingValue<T>>(seq);
        builder.AddAttribute(__seq0, "Value", __arg0);
        builder.AddAttribute(__seq1, "ChildContent", __arg1);
        builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591