# Pixiv Wallpaper for Windows 10
[<img src='https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Get_it_from_Microsoft_Badge.svg/320px-Get_it_from_Microsoft_Badge.svg.png' alt='English badge' width=320 height=116/>](https://www.microsoft.com/zh-cn/p/pixiv-wallpaper-for-windows-10/9n71rkg8kcvc?activetab=pivot:overviewtab)

抓取pixiv.net的图片并设置为Windows 10桌面壁纸的UWP APP，需要在Windows 10 1903以上的版本系统中运行。

依赖部分NuGet包，包含简体中文，日文，英文三种语言。

有"Pixiv 书签"、"已关注用户的插画更新"以及"Pixiv 推荐"三种模式。  

**三种模式均需要在设置页中输入pixiv账号密码后方可使用**

PixivCS登录模式采用[PixivCS](https://github.com/tobiichiamane/pixivcs/blob/master/PixivAppAPI.cs/ "PixivCS") API，内部大部分使用ios客户端api进行通信。PixivCS模式的代码参考了[pixivUWP](https://github.com/tobiichiamane/pixivfs-uwp/ "pixiv-uwp")项目。十分感谢[鱼](https://github.com/tobiichiamane)在技术上的指导与帮助~  

**现已完全弃用web api，所有模式均使用PixivCS api**

## 直连模式
目前三种模式均支持在中国大陆地区直连pixiv，输入账号密码后可以直接使用。但是图片加载的速度会依个人网络环境而不同，大的图片可能需要有几分钟的加载时间。依旧推荐解除UWP Loopback限制后使用代理服务器进行加速使用。

## UWP使用代理
您需要自己准备代理服务器与代理软件，通过代理软件的全局代理模式或者PAC代理模式并 [解除UWP应用Loopback限制](https://sspai.com/post/41137 "UWP loopback")的方式解决UWP应用通过代理连接pixiv.net的问题。从Microsoft Store获取的应用可以直接以管理员模式开启控制台面板输入：  

`checknetisolation loopbackexempt -a -n=63929Shary.PixivWallpaperforWindows10_hzhgmpxe3vqfr`

由于pixiv部分url并未被完全封锁，大部分的PAC文件中并没有写入这部分url，可能会出现代理开启的情况下图片加载速度还是很慢的情况。因此在PAC代理模式下，推荐手动将pixiv图床的url “i.pximg.net”与“pixivsketch.net”添加进pac列表中以加速图片下载速度。  

## 未来更新计划
单独做一个独立的登录页面和已登录用户页面，把控制逻辑弄得简单一些。。  

## 关于原始项目
初代版本的是由[democyann](https://github.com/democyann)与我共同开发的。(~~实际上当初我只是一个只能写UI和部分简单逻辑还要被琉璃大改逻辑的菜鸡~~)。democyann在2017年之后因为个人原因不再管理此项目，后续开发与维护都由我个人在分支relife完成。在与democyann进行邮件沟通后，她建议我建立自己的代码仓库继续维护此项目。因此今后的Pixiv Wallpaper for Windows 10更新与维护将在此仓库完成，Microsoft Store上面发布的版本也会与此仓库的版本保持一致。

此处为原项目[Pixiv_Wallpaper_for_Win10](https://github.com/democyann/Pixiv_Wallpaper_for_Win10)，已将分支relife与master合并，代码不会再更新。
