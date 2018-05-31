# Getting started

Android client for the [backend](https://github.com/CheckiePy/CheckiePyBackend).

### 1. Prerequisites

* Visual Studio or Xamarin Studio

* Xamarin

### 2. Build and run

##### 2.1. Open the solution

Open file [CheckiePyMobile.sln](CheckiePyMobile.sln) with Visual Studio or Xamarin Studio and run build.

##### 2.2. Setup secrets

Create in folder [CheckiePyMobile/CheckiePyMobile/Helpers](CheckiePyMobile/CheckiePyMobile/Helpers) file `SecretKeeper.cs` that contents:

```csharp
using System;
namespace CheckiePyMobile.Helpers
{
    public static class SecretKeeper
    {
        public static string MOBILE_CENTER_API_KEY = "";
    }
}

```

Here you can set a key from [AppCenter](https://appcenter.ms/) to get app usage analytics.

### 3. Screenshots

| Login | Repositories | New code style |
| ----- | ----- | ----- |
| ![Login](/screenshots/img1.webp) | ![Repositories](/screenshots/img2.webp) | ![New codes tyle](/screenshots/img3.webp) |

# License

[MIT](/LICENSE)