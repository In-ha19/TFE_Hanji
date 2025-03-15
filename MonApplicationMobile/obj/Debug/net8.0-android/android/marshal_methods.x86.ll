; ModuleID = 'marshal_methods.x86.ll'
source_filename = "marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [352 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [698 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 67
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 66
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 107
	i32 26230656, ; 3: Microsoft.Extensions.DependencyModel => 0x1903f80 => 195
	i32 32687329, ; 4: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 267
	i32 34715100, ; 5: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 302
	i32 34839235, ; 6: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 47
	i32 39109920, ; 7: Newtonsoft.Json.dll => 0x254c520 => 210
	i32 39485524, ; 8: System.Net.WebSockets.dll => 0x25a8054 => 79
	i32 40744412, ; 9: Xamarin.AndroidX.Camera.Lifecycle.dll => 0x26db5dc => 238
	i32 42639949, ; 10: System.Threading.Thread => 0x28aa24d => 141
	i32 66541672, ; 11: System.Diagnostics.StackTrace => 0x3f75868 => 29
	i32 67008169, ; 12: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 346
	i32 68219467, ; 13: System.Security.Cryptography.Primitives => 0x410f24b => 123
	i32 72070932, ; 14: Microsoft.Maui.Graphics.dll => 0x44bb714 => 209
	i32 82292897, ; 15: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 101
	i32 83768722, ; 16: Microsoft.AspNetCore.Cryptography.Internal => 0x4fe3592 => 173
	i32 101534019, ; 17: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 285
	i32 117431740, ; 18: System.Runtime.InteropServices => 0x6ffddbc => 106
	i32 120558881, ; 19: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 285
	i32 122350210, ; 20: System.Threading.Channels.dll => 0x74aea82 => 221
	i32 134690465, ; 21: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 306
	i32 142721839, ; 22: System.Net.WebHeaderCollection => 0x881c32f => 76
	i32 149972175, ; 23: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 123
	i32 159306688, ; 24: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 25: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 241
	i32 176265551, ; 26: System.ServiceProcess => 0xa81994f => 131
	i32 182336117, ; 27: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 287
	i32 184328833, ; 28: System.ValueTuple.dll => 0xafca281 => 147
	i32 195452805, ; 29: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 343
	i32 199333315, ; 30: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 344
	i32 205061960, ; 31: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 32: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 235
	i32 220171995, ; 33: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 221063263, ; 34: Microsoft.AspNetCore.Http.Connections.Client => 0xd2d285f => 175
	i32 230216969, ; 35: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 261
	i32 230752869, ; 36: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 37: System.Linq.Parallel => 0xdcb05c4 => 58
	i32 231814094, ; 38: System.Globalization => 0xdd133ce => 41
	i32 246610117, ; 39: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 90
	i32 261689757, ; 40: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 244
	i32 276479776, ; 41: System.Threading.Timer.dll => 0x107abf20 => 143
	i32 278686392, ; 42: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 263
	i32 280482487, ; 43: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 260
	i32 280992041, ; 44: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 315
	i32 291076382, ; 45: System.IO.Pipes.AccessControl.dll => 0x1159791e => 53
	i32 298918909, ; 46: System.Net.Ping.dll => 0x11d123fd => 68
	i32 317674968, ; 47: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 343
	i32 318968648, ; 48: Xamarin.AndroidX.Activity.dll => 0x13031348 => 226
	i32 321597661, ; 49: System.Numerics => 0x132b30dd => 82
	i32 336156722, ; 50: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 328
	i32 342366114, ; 51: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 262
	i32 347068432, ; 52: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 214
	i32 348048101, ; 53: Microsoft.AspNetCore.Http.Connections.Common.dll => 0x14becae5 => 176
	i32 356389973, ; 54: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 327
	i32 360082299, ; 55: System.ServiceModel.Web => 0x15766b7b => 130
	i32 367780167, ; 56: System.IO.Pipes => 0x15ebe147 => 54
	i32 374914964, ; 57: System.Transactions.Local => 0x1658bf94 => 145
	i32 375677976, ; 58: System.Net.ServicePoint.dll => 0x16646418 => 73
	i32 379916513, ; 59: System.Threading.Thread.dll => 0x16a510e1 => 141
	i32 385762202, ; 60: System.Memory.dll => 0x16fe439a => 61
	i32 392610295, ; 61: System.Threading.ThreadPool.dll => 0x1766c1f7 => 142
	i32 395744057, ; 62: _Microsoft.Android.Resource.Designer => 0x17969339 => 348
	i32 403441872, ; 63: WindowsBase => 0x180c08d0 => 161
	i32 435591531, ; 64: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 339
	i32 441335492, ; 65: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 245
	i32 442565967, ; 66: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 67: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 258
	i32 451504562, ; 68: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 124
	i32 456227837, ; 69: System.Web.HttpUtility.dll => 0x1b317bfd => 148
	i32 458494020, ; 70: Microsoft.AspNetCore.SignalR.Common.dll => 0x1b541044 => 180
	i32 459347974, ; 71: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 112
	i32 465846621, ; 72: mscorlib => 0x1bc4415d => 162
	i32 469710990, ; 73: System.dll => 0x1bff388e => 160
	i32 476646585, ; 74: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 260
	i32 486930444, ; 75: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 273
	i32 498788369, ; 76: System.ObjectModel => 0x1dbae811 => 83
	i32 500358224, ; 77: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 326
	i32 503918385, ; 78: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 320
	i32 513247710, ; 79: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 203
	i32 526420162, ; 80: System.Transactions.dll => 0x1f6088c2 => 146
	i32 527452488, ; 81: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 306
	i32 530272170, ; 82: System.Linq.Queryable => 0x1f9b4faa => 59
	i32 539058512, ; 83: Microsoft.Extensions.Logging => 0x20216150 => 199
	i32 540030774, ; 84: System.IO.FileSystem.dll => 0x20303736 => 50
	i32 545304856, ; 85: System.Runtime.Extensions => 0x2080b118 => 102
	i32 546455878, ; 86: System.Runtime.Serialization.Xml => 0x20924146 => 113
	i32 548916678, ; 87: Microsoft.Bcl.AsyncInterfaces => 0x20b7cdc6 => 182
	i32 549171840, ; 88: System.Globalization.Calendars => 0x20bbb280 => 39
	i32 557405415, ; 89: Jsr305Binding => 0x213954e7 => 299
	i32 569601784, ; 90: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 296
	i32 577335427, ; 91: System.Security.Cryptography.Cng => 0x22697083 => 119
	i32 592146354, ; 92: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 334
	i32 601371474, ; 93: System.IO.IsolatedStorage.dll => 0x23d83352 => 51
	i32 605376203, ; 94: System.IO.Compression.FileSystem => 0x24154ecb => 43
	i32 613668793, ; 95: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 118
	i32 627609679, ; 96: Xamarin.AndroidX.CustomView => 0x2568904f => 250
	i32 627931235, ; 97: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 332
	i32 639843206, ; 98: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 256
	i32 643868501, ; 99: System.Net => 0x2660a755 => 80
	i32 662205335, ; 100: System.Text.Encodings.Web.dll => 0x27787397 => 219
	i32 663517072, ; 101: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 292
	i32 666292255, ; 102: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 233
	i32 672442732, ; 103: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 104: System.Net.Security => 0x28bdabca => 72
	i32 688181140, ; 105: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 314
	i32 690569205, ; 106: System.Xml.Linq.dll => 0x29293ff5 => 151
	i32 691348768, ; 107: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 308
	i32 693804605, ; 108: System.Windows => 0x295a9e3d => 150
	i32 699345723, ; 109: System.Reflection.Emit => 0x29af2b3b => 91
	i32 700284507, ; 110: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 303
	i32 700358131, ; 111: System.IO.Compression.ZipFile => 0x29be9df3 => 44
	i32 706645707, ; 112: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 329
	i32 709557578, ; 113: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 317
	i32 720511267, ; 114: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 307
	i32 722857257, ; 115: System.Runtime.Loader.dll => 0x2b15ed29 => 108
	i32 735137430, ; 116: System.Security.SecureString.dll => 0x2bd14e96 => 128
	i32 748832960, ; 117: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 212
	i32 752232764, ; 118: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 119: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 223
	i32 759454413, ; 120: System.Net.Requests => 0x2d445acd => 71
	i32 762598435, ; 121: System.IO.Pipes.dll => 0x2d745423 => 54
	i32 775507847, ; 122: System.IO.Compression => 0x2e394f87 => 45
	i32 777317022, ; 123: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 338
	i32 782533833, ; 124: Xamarin.Google.AutoValue.Annotations.dll => 0x2ea484c9 => 298
	i32 789151979, ; 125: Microsoft.Extensions.Options => 0x2f0980eb => 202
	i32 790371945, ; 126: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 251
	i32 804715423, ; 127: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 128: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 265
	i32 815077618, ; 129: MyApp.Shared => 0x309518f2 => 347
	i32 823281589, ; 130: System.Private.Uri.dll => 0x311247b5 => 85
	i32 830298997, ; 131: System.IO.Compression.Brotli => 0x317d5b75 => 42
	i32 832635846, ; 132: System.Xml.XPath.dll => 0x31a103c6 => 156
	i32 832711436, ; 133: Microsoft.AspNetCore.SignalR.Protocols.Json.dll => 0x31a22b0c => 181
	i32 834051424, ; 134: System.Net.Quic => 0x31b69d60 => 70
	i32 839353180, ; 135: ZXing.Net.MAUI.Controls.dll => 0x3207835c => 312
	i32 843511501, ; 136: Xamarin.AndroidX.Print => 0x3246f6cd => 278
	i32 865465478, ; 137: zxing.dll => 0x3395f486 => 310
	i32 873119928, ; 138: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 139: System.Globalization.dll => 0x34505120 => 41
	i32 878954865, ; 140: System.Net.Http.Json => 0x3463c971 => 62
	i32 904024072, ; 141: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 142: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 52
	i32 926902833, ; 143: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 341
	i32 928116545, ; 144: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 302
	i32 952186615, ; 145: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 104
	i32 955402788, ; 146: Newtonsoft.Json => 0x38f24a24 => 210
	i32 956575887, ; 147: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 307
	i32 966729478, ; 148: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 300
	i32 967690846, ; 149: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 262
	i32 975236339, ; 150: System.Diagnostics.Tracing => 0x3a20ecf3 => 33
	i32 975874589, ; 151: System.Xml.XDocument => 0x3a2aaa1d => 154
	i32 986514023, ; 152: System.Private.DataContractSerialization.dll => 0x3acd0267 => 84
	i32 987214855, ; 153: System.Diagnostics.Tools => 0x3ad7b407 => 31
	i32 992768348, ; 154: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 155: System.IO.FileSystem => 0x3b45fb35 => 50
	i32 1001831731, ; 156: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 55
	i32 1012816738, ; 157: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 282
	i32 1019214401, ; 158: System.Drawing => 0x3cbffa41 => 35
	i32 1028951442, ; 159: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 194
	i32 1029334545, ; 160: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 316
	i32 1031528504, ; 161: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 301
	i32 1035644815, ; 162: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 231
	i32 1036536393, ; 163: System.Drawing.Primitives.dll => 0x3dc84a49 => 34
	i32 1044663988, ; 164: System.Linq.Expressions.dll => 0x3e444eb4 => 57
	i32 1052210849, ; 165: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 269
	i32 1058641855, ; 166: Microsoft.AspNetCore.Http.Connections.Common => 0x3f1997bf => 176
	i32 1061503568, ; 167: Xamarin.Google.AutoValue.Annotations => 0x3f454250 => 298
	i32 1067306892, ; 168: GoogleGson => 0x3f9dcf8c => 171
	i32 1082857460, ; 169: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 170: Xamarin.Kotlin.StdLib => 0x409e66d8 => 304
	i32 1098259244, ; 171: System => 0x41761b2c => 160
	i32 1118262833, ; 172: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 329
	i32 1121599056, ; 173: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 268
	i32 1127624469, ; 174: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 201
	i32 1135815421, ; 175: Microsoft.AspNetCore.Cryptography.KeyDerivation.dll => 0x43b32afd => 174
	i32 1149092582, ; 176: Xamarin.AndroidX.Window => 0x447dc2e6 => 295
	i32 1157931901, ; 177: Microsoft.EntityFrameworkCore.Abstractions => 0x4504a37d => 186
	i32 1160313973, ; 178: System.Net.ServerSentEvents.dll => 0x4528fc75 => 218
	i32 1168523401, ; 179: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 335
	i32 1170634674, ; 180: System.Web.dll => 0x45c677b2 => 149
	i32 1175144683, ; 181: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 291
	i32 1175944061, ; 182: Camera.MAUI => 0x46177b7d => 169
	i32 1178241025, ; 183: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 276
	i32 1202000627, ; 184: Microsoft.EntityFrameworkCore.Abstractions.dll => 0x47a512f3 => 186
	i32 1203215381, ; 185: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 333
	i32 1204270330, ; 186: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 233
	i32 1204575371, ; 187: Microsoft.Extensions.Caching.Memory.dll => 0x47cc5c8b => 190
	i32 1208641965, ; 188: System.Diagnostics.Process => 0x480a69ad => 28
	i32 1219128291, ; 189: System.IO.IsolatedStorage => 0x48aa6be3 => 51
	i32 1233093933, ; 190: Microsoft.AspNetCore.SignalR.Client.Core.dll => 0x497f852d => 179
	i32 1234928153, ; 191: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 331
	i32 1243150071, ; 192: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 296
	i32 1253011324, ; 193: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1260983243, ; 194: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 315
	i32 1264511973, ; 195: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 286
	i32 1267360935, ; 196: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 290
	i32 1273260888, ; 197: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 242
	i32 1275534314, ; 198: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 308
	i32 1278448581, ; 199: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 230
	i32 1292207520, ; 200: SQLitePCLRaw.core.dll => 0x4d0585a0 => 213
	i32 1293217323, ; 201: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 253
	i32 1309188875, ; 202: System.Private.DataContractSerialization => 0x4e08a30b => 84
	i32 1322716291, ; 203: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 295
	i32 1324164729, ; 204: System.Linq => 0x4eed2679 => 60
	i32 1335329327, ; 205: System.Runtime.Serialization.Json.dll => 0x4f97822f => 111
	i32 1364015309, ; 206: System.IO => 0x514d38cd => 56
	i32 1373134921, ; 207: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 345
	i32 1376866003, ; 208: Xamarin.AndroidX.SavedState => 0x52114ed3 => 282
	i32 1379779777, ; 209: System.Resources.ResourceManager => 0x523dc4c1 => 98
	i32 1402170036, ; 210: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 211: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 246
	i32 1408764838, ; 212: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 110
	i32 1411638395, ; 213: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 100
	i32 1414043276, ; 214: Microsoft.AspNetCore.Connections.Abstractions.dll => 0x5448968c => 172
	i32 1422545099, ; 215: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 101
	i32 1430672901, ; 216: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 313
	i32 1434145427, ; 217: System.Runtime.Handles => 0x557b5293 => 103
	i32 1435222561, ; 218: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 300
	i32 1439761251, ; 219: System.Net.Quic.dll => 0x55d10363 => 70
	i32 1452070440, ; 220: System.Formats.Asn1.dll => 0x568cd628 => 37
	i32 1453312822, ; 221: System.Diagnostics.Tools.dll => 0x569fcb36 => 31
	i32 1457743152, ; 222: System.Runtime.Extensions.dll => 0x56e36530 => 102
	i32 1458022317, ; 223: System.Net.Security.dll => 0x56e7a7ad => 72
	i32 1461004990, ; 224: es\Microsoft.Maui.Controls.resources => 0x57152abe => 319
	i32 1461234159, ; 225: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 226: System.Security.Cryptography.OpenSsl => 0x57201017 => 122
	i32 1462112819, ; 227: System.IO.Compression.dll => 0x57261233 => 45
	i32 1469204771, ; 228: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 232
	i32 1470490898, ; 229: Microsoft.Extensions.Primitives => 0x57a5e912 => 203
	i32 1479771757, ; 230: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 231: System.IO.Compression.Brotli.dll => 0x583e844f => 42
	i32 1487239319, ; 232: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1490025113, ; 233: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 283
	i32 1490351284, ; 234: Microsoft.Data.Sqlite.dll => 0x58d4f4b4 => 184
	i32 1493001747, ; 235: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 323
	i32 1514721132, ; 236: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 318
	i32 1536373174, ; 237: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 30
	i32 1543031311, ; 238: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 135
	i32 1543355203, ; 239: System.Reflection.Emit.dll => 0x5bfdbb43 => 91
	i32 1550322496, ; 240: System.Reflection.Extensions.dll => 0x5c680b40 => 92
	i32 1551623176, ; 241: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 338
	i32 1565862583, ; 242: System.IO.FileSystem.Primitives => 0x5d552ab7 => 48
	i32 1566207040, ; 243: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 137
	i32 1573704789, ; 244: System.Runtime.Serialization.Json => 0x5dccd455 => 111
	i32 1580037396, ; 245: System.Threading.Overlapped => 0x5e2d7514 => 136
	i32 1582372066, ; 246: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 252
	i32 1592978981, ; 247: System.Runtime.Serialization.dll => 0x5ef2ee25 => 114
	i32 1597949149, ; 248: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 301
	i32 1601112923, ; 249: System.Xml.Serialization => 0x5f6f0b5b => 153
	i32 1604827217, ; 250: System.Net.WebClient => 0x5fa7b851 => 75
	i32 1618516317, ; 251: System.Net.WebSockets.Client.dll => 0x6078995d => 78
	i32 1622152042, ; 252: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 272
	i32 1622358360, ; 253: System.Dynamic.Runtime => 0x60b33958 => 36
	i32 1624863272, ; 254: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 294
	i32 1635184631, ; 255: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 256
	i32 1636350590, ; 256: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 249
	i32 1639515021, ; 257: System.Net.Http.dll => 0x61b9038d => 63
	i32 1639986890, ; 258: System.Text.RegularExpressions => 0x61c036ca => 135
	i32 1641389582, ; 259: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 260: System.Runtime => 0x62c6282e => 115
	i32 1658241508, ; 261: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 288
	i32 1658251792, ; 262: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 297
	i32 1670060433, ; 263: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 244
	i32 1675553242, ; 264: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 47
	i32 1677501392, ; 265: System.Net.Primitives.dll => 0x63fca3d0 => 69
	i32 1678508291, ; 266: System.Net.WebSockets => 0x640c0103 => 79
	i32 1679769178, ; 267: System.Security.Cryptography => 0x641f3e5a => 125
	i32 1688112883, ; 268: Microsoft.Data.Sqlite => 0x649e8ef3 => 184
	i32 1689493916, ; 269: Microsoft.EntityFrameworkCore.dll => 0x64b3a19c => 185
	i32 1691477237, ; 270: System.Reflection.Metadata => 0x64d1e4f5 => 93
	i32 1696967625, ; 271: System.Security.Cryptography.Csp => 0x6525abc9 => 120
	i32 1698840827, ; 272: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 305
	i32 1701541528, ; 273: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1711441057, ; 274: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 214
	i32 1720223769, ; 275: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 265
	i32 1726116996, ; 276: System.Reflection.dll => 0x66e27484 => 96
	i32 1728033016, ; 277: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 27
	i32 1729485958, ; 278: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 240
	i32 1736233607, ; 279: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 336
	i32 1739079468, ; 280: MonApplicationMobile => 0x67a83f2c => 0
	i32 1743415430, ; 281: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 314
	i32 1744735666, ; 282: System.Transactions.Local.dll => 0x67fe8db2 => 145
	i32 1746115085, ; 283: System.IO.Pipelines.dll => 0x68139a0d => 217
	i32 1746316138, ; 284: Mono.Android.Export => 0x6816ab6a => 165
	i32 1750313021, ; 285: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 286: System.Resources.Reader.dll => 0x68cc9d1e => 97
	i32 1763938596, ; 287: System.Diagnostics.TraceSource.dll => 0x69239124 => 32
	i32 1765942094, ; 288: System.Reflection.Extensions => 0x6942234e => 92
	i32 1766324549, ; 289: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 287
	i32 1770582343, ; 290: Microsoft.Extensions.Logging.dll => 0x6988f147 => 199
	i32 1776026572, ; 291: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 292: System.Globalization.Extensions.dll => 0x69ec0683 => 40
	i32 1780572499, ; 293: Mono.Android.Runtime.dll => 0x6a216153 => 166
	i32 1782862114, ; 294: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 330
	i32 1788241197, ; 295: Xamarin.AndroidX.Fragment => 0x6a96652d => 258
	i32 1793755602, ; 296: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 322
	i32 1796167890, ; 297: Microsoft.Bcl.AsyncInterfaces.dll => 0x6b0f58d2 => 182
	i32 1808609942, ; 298: Xamarin.AndroidX.Loader => 0x6bcd3296 => 272
	i32 1813058853, ; 299: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 304
	i32 1813201214, ; 300: Xamarin.Google.Android.Material => 0x6c13413e => 297
	i32 1818569960, ; 301: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 277
	i32 1818787751, ; 302: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1820883333, ; 303: Microsoft.AspNetCore.Cryptography.Internal.dll => 0x6c887985 => 173
	i32 1824175904, ; 304: System.Text.Encoding.Extensions => 0x6cbab720 => 133
	i32 1824722060, ; 305: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 110
	i32 1828688058, ; 306: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 200
	i32 1842015223, ; 307: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 342
	i32 1847515442, ; 308: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 223
	i32 1853025655, ; 309: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 339
	i32 1858542181, ; 310: System.Linq.Expressions => 0x6ec71a65 => 57
	i32 1870277092, ; 311: System.Reflection.Primitives => 0x6f7a29e4 => 94
	i32 1875935024, ; 312: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 321
	i32 1879696579, ; 313: System.Formats.Tar.dll => 0x7009e4c3 => 38
	i32 1885316902, ; 314: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 234
	i32 1886040351, ; 315: Microsoft.EntityFrameworkCore.Sqlite.dll => 0x706ab11f => 188
	i32 1888955245, ; 316: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 317: System.Reflection.Metadata.dll => 0x70a66bdd => 93
	i32 1898237753, ; 318: System.Reflection.DispatchProxy => 0x7124cf39 => 88
	i32 1900610850, ; 319: System.Resources.ResourceManager.dll => 0x71490522 => 98
	i32 1910275211, ; 320: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1932718519, ; 321: Microsoft.Bcl.TimeProvider => 0x7332f1b7 => 183
	i32 1939592360, ; 322: System.Private.Xml.Linq => 0x739bd4a8 => 86
	i32 1945717188, ; 323: Microsoft.AspNetCore.SignalR.Client.Core => 0x73f949c4 => 179
	i32 1956758971, ; 324: System.Resources.Writer => 0x74a1c5bb => 99
	i32 1961813231, ; 325: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 284
	i32 1967334205, ; 326: Microsoft.AspNetCore.SignalR.Common => 0x7543233d => 180
	i32 1968388702, ; 327: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 191
	i32 1978435800, ; 328: System.Net.ServerSentEvents => 0x75ec88d8 => 218
	i32 1983156543, ; 329: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 305
	i32 1985761444, ; 330: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 225
	i32 1991044029, ; 331: Microsoft.Extensions.Identity.Core.dll => 0x76acebbd => 197
	i32 1991196148, ; 332: Microsoft.AspNetCore.Identity.EntityFrameworkCore.dll => 0x76af3df4 => 177
	i32 2003115576, ; 333: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 318
	i32 2011961780, ; 334: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2014489277, ; 335: Microsoft.EntityFrameworkCore.Sqlite => 0x7812aabd => 188
	i32 2019465201, ; 336: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 269
	i32 2025202353, ; 337: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 313
	i32 2031763787, ; 338: Xamarin.Android.Glide => 0x791a414b => 222
	i32 2045470958, ; 339: System.Private.Xml => 0x79eb68ee => 87
	i32 2055257422, ; 340: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 264
	i32 2060060697, ; 341: System.Windows.dll => 0x7aca0819 => 150
	i32 2066184531, ; 342: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 317
	i32 2070888862, ; 343: System.Diagnostics.TraceSource => 0x7b6f419e => 32
	i32 2079903147, ; 344: System.Runtime.dll => 0x7bf8cdab => 115
	i32 2090596640, ; 345: System.Numerics.Vectors => 0x7c9bf920 => 81
	i32 2103459038, ; 346: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 215
	i32 2127167465, ; 347: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 348: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 349: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 158
	i32 2146852085, ; 350: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2159891885, ; 351: Microsoft.Maui => 0x80bd55ad => 207
	i32 2169148018, ; 352: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 325
	i32 2181898931, ; 353: Microsoft.Extensions.Options.dll => 0x820d22b3 => 202
	i32 2192057212, ; 354: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 200
	i32 2193016926, ; 355: System.ObjectModel.dll => 0x82b6c85e => 83
	i32 2197979891, ; 356: Microsoft.Extensions.DependencyModel.dll => 0x830282f3 => 195
	i32 2201107256, ; 357: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 309
	i32 2201231467, ; 358: System.Net.Http => 0x8334206b => 63
	i32 2207618523, ; 359: it\Microsoft.Maui.Controls.resources => 0x839595db => 327
	i32 2217644978, ; 360: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 291
	i32 2222056684, ; 361: System.Threading.Tasks.Parallel => 0x8471e4ec => 139
	i32 2225453300, ; 362: Camera.MAUI.ZXing => 0x84a5b8f4 => 170
	i32 2229158877, ; 363: Microsoft.Extensions.Features.dll => 0x84de43dd => 196
	i32 2244775296, ; 364: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 273
	i32 2252106437, ; 365: System.Xml.Serialization.dll => 0x863c6ac5 => 153
	i32 2252897993, ; 366: Microsoft.EntityFrameworkCore => 0x86487ec9 => 185
	i32 2256313426, ; 367: System.Globalization.Extensions => 0x867c9c52 => 40
	i32 2265110946, ; 368: System.Security.AccessControl.dll => 0x8702d9a2 => 116
	i32 2266799131, ; 369: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 192
	i32 2267999099, ; 370: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 224
	i32 2270573516, ; 371: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 321
	i32 2274912384, ; 372: Microsoft.Extensions.Identity.Stores => 0x87986880 => 198
	i32 2279755925, ; 373: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 280
	i32 2293034957, ; 374: System.ServiceModel.Web.dll => 0x88acefcd => 130
	i32 2295906218, ; 375: System.Net.Sockets => 0x88d8bfaa => 74
	i32 2298471582, ; 376: System.Net.Mail => 0x88ffe49e => 65
	i32 2303942373, ; 377: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 331
	i32 2305521784, ; 378: System.Private.CoreLib.dll => 0x896b7878 => 168
	i32 2315684594, ; 379: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 228
	i32 2319144366, ; 380: Microsoft.AspNetCore.SignalR.Client => 0x8a3b55ae => 178
	i32 2320631194, ; 381: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 139
	i32 2340441535, ; 382: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 105
	i32 2344264397, ; 383: System.ValueTuple => 0x8bbaa2cd => 147
	i32 2353062107, ; 384: System.Net.Primitives => 0x8c40e0db => 69
	i32 2368005991, ; 385: System.Xml.ReaderWriter.dll => 0x8d24e767 => 152
	i32 2371007202, ; 386: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 191
	i32 2378619854, ; 387: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 120
	i32 2383496789, ; 388: System.Security.Principal.Windows.dll => 0x8e114655 => 126
	i32 2395872292, ; 389: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 326
	i32 2401565422, ; 390: System.Web.HttpUtility => 0x8f24faee => 148
	i32 2403452196, ; 391: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 255
	i32 2406371028, ; 392: Microsoft.Extensions.Identity.Stores.dll => 0x8f6e4ed4 => 198
	i32 2421380589, ; 393: System.Threading.Tasks.Dataflow => 0x905355ed => 137
	i32 2423080555, ; 394: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 242
	i32 2427813419, ; 395: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 323
	i32 2435356389, ; 396: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 397: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 398: System.Text.Encoding.dll => 0x924edee6 => 134
	i32 2458678730, ; 399: System.Net.Sockets.dll => 0x928c75ca => 74
	i32 2459001652, ; 400: System.Linq.Parallel.dll => 0x92916334 => 58
	i32 2465273461, ; 401: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 212
	i32 2465532216, ; 402: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 245
	i32 2471841756, ; 403: netstandard.dll => 0x93554fdc => 163
	i32 2475788418, ; 404: Java.Interop.dll => 0x93918882 => 164
	i32 2480646305, ; 405: Microsoft.Maui.Controls => 0x93dba8a1 => 205
	i32 2483903535, ; 406: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 407: System.Net.ServicePoint => 0x94147f61 => 73
	i32 2490993605, ; 408: System.AppContext.dll => 0x94798bc5 => 6
	i32 2501346920, ; 409: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2505896520, ; 410: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 267
	i32 2522472828, ; 411: Xamarin.Android.Glide.dll => 0x9659e17c => 222
	i32 2538310050, ; 412: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 90
	i32 2550873716, ; 413: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 324
	i32 2562349572, ; 414: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 415: System.Text.Encodings.Web => 0x9930ee42 => 219
	i32 2581783588, ; 416: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 268
	i32 2581819634, ; 417: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 290
	i32 2585220780, ; 418: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 133
	i32 2585805581, ; 419: System.Net.Ping => 0x9a20430d => 68
	i32 2589602615, ; 420: System.Threading.ThreadPool => 0x9a5a3337 => 142
	i32 2593496499, ; 421: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 333
	i32 2605712449, ; 422: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 309
	i32 2615233544, ; 423: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 259
	i32 2616218305, ; 424: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 201
	i32 2617129537, ; 425: System.Private.Xml.dll => 0x9bfe3a41 => 87
	i32 2618712057, ; 426: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 95
	i32 2620871830, ; 427: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 249
	i32 2624644809, ; 428: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 254
	i32 2626831493, ; 429: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 328
	i32 2627185994, ; 430: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 30
	i32 2629843544, ; 431: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 44
	i32 2633051222, ; 432: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 263
	i32 2634653062, ; 433: Microsoft.EntityFrameworkCore.Relational.dll => 0x9d099d86 => 187
	i32 2637500010, ; 434: Microsoft.Extensions.Features => 0x9d350e6a => 196
	i32 2663391936, ; 435: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 224
	i32 2663698177, ; 436: System.Runtime.Loader => 0x9ec4cf01 => 108
	i32 2664396074, ; 437: System.Xml.XDocument.dll => 0x9ecf752a => 154
	i32 2665622720, ; 438: System.Drawing.Primitives => 0x9ee22cc0 => 34
	i32 2676780864, ; 439: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2686887180, ; 440: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 113
	i32 2693849962, ; 441: System.IO.dll => 0xa090e36a => 56
	i32 2701096212, ; 442: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 288
	i32 2715334215, ; 443: System.Threading.Tasks.dll => 0xa1d8b647 => 140
	i32 2717744543, ; 444: System.Security.Claims => 0xa1fd7d9f => 117
	i32 2719963679, ; 445: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 119
	i32 2724373263, ; 446: System.Runtime.Numerics.dll => 0xa262a30f => 109
	i32 2732626843, ; 447: Xamarin.AndroidX.Activity => 0xa2e0939b => 226
	i32 2735172069, ; 448: System.Threading.Channels => 0xa30769e5 => 221
	i32 2737747696, ; 449: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 232
	i32 2740948882, ; 450: System.IO.Pipes.AccessControl => 0xa35f8f92 => 53
	i32 2748088231, ; 451: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 104
	i32 2752995522, ; 452: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 334
	i32 2758225723, ; 453: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 206
	i32 2764765095, ; 454: Microsoft.Maui.dll => 0xa4caf7a7 => 207
	i32 2765824710, ; 455: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 132
	i32 2770495804, ; 456: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 303
	i32 2778768386, ; 457: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 293
	i32 2779977773, ; 458: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 281
	i32 2785988530, ; 459: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 340
	i32 2788224221, ; 460: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 259
	i32 2801831435, ; 461: Microsoft.Maui.Graphics => 0xa7008e0b => 209
	i32 2803228030, ; 462: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 155
	i32 2806116107, ; 463: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 319
	i32 2810250172, ; 464: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 246
	i32 2819470561, ; 465: System.Xml.dll => 0xa80db4e1 => 159
	i32 2821205001, ; 466: System.ServiceProcess.dll => 0xa8282c09 => 131
	i32 2821294376, ; 467: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 281
	i32 2824502124, ; 468: System.Xml.XmlDocument => 0xa85a7b6c => 157
	i32 2831556043, ; 469: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 332
	i32 2838993487, ; 470: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 270
	i32 2847789619, ; 471: Microsoft.EntityFrameworkCore.Relational => 0xa9bdd233 => 187
	i32 2849599387, ; 472: System.Threading.Overlapped.dll => 0xa9d96f9b => 136
	i32 2853208004, ; 473: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 293
	i32 2855708567, ; 474: Xamarin.AndroidX.Transition => 0xaa36a797 => 289
	i32 2861098320, ; 475: Mono.Android.Export.dll => 0xaa88e550 => 165
	i32 2861189240, ; 476: Microsoft.Maui.Essentials => 0xaa8a4878 => 208
	i32 2870099610, ; 477: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 227
	i32 2875164099, ; 478: Jsr305Binding.dll => 0xab5f85c3 => 299
	i32 2875220617, ; 479: System.Globalization.Calendars.dll => 0xab606289 => 39
	i32 2875347124, ; 480: Microsoft.AspNetCore.Http.Connections.Client.dll => 0xab6250b4 => 175
	i32 2884993177, ; 481: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 257
	i32 2887636118, ; 482: System.Net.dll => 0xac1dd496 => 80
	i32 2899753641, ; 483: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 55
	i32 2900621748, ; 484: System.Dynamic.Runtime.dll => 0xace3f9b4 => 36
	i32 2901442782, ; 485: System.Reflection => 0xacf080de => 96
	i32 2905242038, ; 486: mscorlib.dll => 0xad2a79b6 => 162
	i32 2909740682, ; 487: System.Private.CoreLib => 0xad6f1e8a => 168
	i32 2916838712, ; 488: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 294
	i32 2919462931, ; 489: System.Numerics.Vectors.dll => 0xae037813 => 81
	i32 2921128767, ; 490: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 229
	i32 2936416060, ; 491: System.Resources.Reader => 0xaf06273c => 97
	i32 2940926066, ; 492: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 29
	i32 2942453041, ; 493: System.Xml.XPath.XDocument => 0xaf624531 => 155
	i32 2959614098, ; 494: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2965157864, ; 495: Xamarin.AndroidX.Camera.View => 0xb0bcb7e8 => 239
	i32 2968338931, ; 496: System.Security.Principal.Windows => 0xb0ed41f3 => 126
	i32 2972252294, ; 497: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 118
	i32 2978675010, ; 498: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 253
	i32 2987532451, ; 499: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 284
	i32 2991449226, ; 500: Xamarin.AndroidX.Camera.Core => 0xb24de48a => 237
	i32 2996846495, ; 501: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 266
	i32 3000842441, ; 502: Xamarin.AndroidX.Camera.View.dll => 0xb2dd38c9 => 239
	i32 3014607031, ; 503: Microsoft.AspNetCore.Cryptography.KeyDerivation => 0xb3af40b7 => 174
	i32 3016983068, ; 504: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 286
	i32 3023353419, ; 505: WindowsBase.dll => 0xb434b64b => 161
	i32 3024354802, ; 506: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 261
	i32 3038032645, ; 507: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 348
	i32 3047751430, ; 508: Xamarin.AndroidX.Camera.Core.dll => 0xb5a8ff06 => 237
	i32 3056245963, ; 509: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 283
	i32 3057625584, ; 510: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 274
	i32 3059408633, ; 511: Mono.Android.Runtime => 0xb65adef9 => 166
	i32 3059793426, ; 512: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3069363400, ; 513: Microsoft.Extensions.Caching.Abstractions.dll => 0xb6f2c4c8 => 189
	i32 3075834255, ; 514: System.Threading.Tasks => 0xb755818f => 140
	i32 3077302341, ; 515: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 325
	i32 3090735792, ; 516: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 124
	i32 3099732863, ; 517: System.Security.Claims.dll => 0xb8c22b7f => 117
	i32 3103600923, ; 518: System.Formats.Asn1 => 0xb8fd311b => 37
	i32 3111772706, ; 519: System.Runtime.Serialization => 0xb979e222 => 114
	i32 3121463068, ; 520: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 46
	i32 3124832203, ; 521: System.Threading.Tasks.Extensions => 0xba4127cb => 138
	i32 3132293585, ; 522: System.Security.AccessControl => 0xbab301d1 => 116
	i32 3147165239, ; 523: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 33
	i32 3148237826, ; 524: GoogleGson.dll => 0xbba64c02 => 171
	i32 3159123045, ; 525: System.Reflection.Primitives.dll => 0xbc4c6465 => 94
	i32 3160747431, ; 526: System.IO.MemoryMappedFiles => 0xbc652da7 => 52
	i32 3178803400, ; 527: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 275
	i32 3181677278, ; 528: MonApplicationMobile.dll => 0xbda48ade => 0
	i32 3192346100, ; 529: System.Security.SecureString => 0xbe4755f4 => 128
	i32 3193515020, ; 530: System.Web => 0xbe592c0c => 149
	i32 3195844289, ; 531: Microsoft.Extensions.Caching.Abstractions => 0xbe7cb6c1 => 189
	i32 3204380047, ; 532: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 533: System.Xml.XmlDocument.dll => 0xbf506931 => 157
	i32 3211777861, ; 534: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 252
	i32 3215347189, ; 535: zxing => 0xbfa64df5 => 310
	i32 3220365878, ; 536: System.Threading => 0xbff2e236 => 144
	i32 3226221578, ; 537: System.Runtime.Handles.dll => 0xc04c3c0a => 103
	i32 3251039220, ; 538: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 88
	i32 3258312781, ; 539: Xamarin.AndroidX.CardView => 0xc235e84d => 240
	i32 3265493905, ; 540: System.Linq.Queryable.dll => 0xc2a37b91 => 59
	i32 3265893370, ; 541: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 138
	i32 3277815716, ; 542: System.Resources.Writer.dll => 0xc35f7fa4 => 99
	i32 3279906254, ; 543: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 544: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3286373667, ; 545: ZXing.Net.MAUI.dll => 0xc3e21523 => 311
	i32 3286872994, ; 546: SQLite-net.dll => 0xc3e9b3a2 => 211
	i32 3290767353, ; 547: System.Security.Cryptography.Encoding => 0xc4251ff9 => 121
	i32 3299363146, ; 548: System.Text.Encoding => 0xc4a8494a => 134
	i32 3303498502, ; 549: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 27
	i32 3305363605, ; 550: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 320
	i32 3316684772, ; 551: System.Net.Requests.dll => 0xc5b097e4 => 71
	i32 3317135071, ; 552: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 250
	i32 3317144872, ; 553: System.Data => 0xc5b79d28 => 24
	i32 3336690922, ; 554: Camera.MAUI.ZXing.dll => 0xc6e1dcea => 170
	i32 3340431453, ; 555: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 234
	i32 3345895724, ; 556: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 279
	i32 3346324047, ; 557: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 276
	i32 3357674450, ; 558: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 337
	i32 3358260929, ; 559: System.Text.Json => 0xc82afec1 => 220
	i32 3360279109, ; 560: SQLitePCLRaw.core => 0xc849ca45 => 213
	i32 3362336904, ; 561: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 227
	i32 3362522851, ; 562: Xamarin.AndroidX.Core => 0xc86c06e3 => 247
	i32 3366347497, ; 563: Java.Interop => 0xc8a662e9 => 164
	i32 3374999561, ; 564: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 280
	i32 3381016424, ; 565: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 316
	i32 3395150330, ; 566: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 100
	i32 3403906625, ; 567: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 122
	i32 3405233483, ; 568: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 251
	i32 3413944578, ; 569: Xamarin.AndroidX.Camera.Camera2.dll => 0xcb7ca902 => 236
	i32 3421910702, ; 570: Xamarin.AndroidX.Camera.Camera2 => 0xcbf636ae => 236
	i32 3428513518, ; 571: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 193
	i32 3429136800, ; 572: System.Xml => 0xcc6479a0 => 159
	i32 3430777524, ; 573: netstandard => 0xcc7d82b4 => 163
	i32 3441283291, ; 574: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 254
	i32 3445260447, ; 575: System.Formats.Tar => 0xcd5a809f => 38
	i32 3452344032, ; 576: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 204
	i32 3463511458, ; 577: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 324
	i32 3466904072, ; 578: Microsoft.AspNetCore.SignalR.Client.dll => 0xcea4c208 => 178
	i32 3471940407, ; 579: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 580: Mono.Android => 0xcf3163e6 => 167
	i32 3479583265, ; 581: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 337
	i32 3484440000, ; 582: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 336
	i32 3485117614, ; 583: System.Text.Json.dll => 0xcfbaacae => 220
	i32 3486566296, ; 584: System.Transactions => 0xcfd0c798 => 146
	i32 3493954962, ; 585: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 243
	i32 3509114376, ; 586: System.Xml.Linq => 0xd128d608 => 151
	i32 3512041038, ; 587: MyApp.Shared.dll => 0xd1557e4e => 347
	i32 3515174580, ; 588: System.Security.dll => 0xd1854eb4 => 129
	i32 3530912306, ; 589: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 590: System.Net.HttpListener => 0xd2ff69f1 => 64
	i32 3560100363, ; 591: System.Threading.Timer => 0xd432d20b => 143
	i32 3570554715, ; 592: System.IO.FileSystem.AccessControl => 0xd4d2575b => 46
	i32 3580758918, ; 593: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 344
	i32 3597029428, ; 594: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 225
	i32 3598340787, ; 595: System.Net.WebSockets.Client => 0xd67a52b3 => 78
	i32 3608519521, ; 596: System.Linq.dll => 0xd715a361 => 60
	i32 3619374377, ; 597: Microsoft.Extensions.Identity.Core => 0xd7bb4529 => 197
	i32 3624195450, ; 598: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 105
	i32 3627220390, ; 599: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 278
	i32 3633644679, ; 600: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 229
	i32 3638274909, ; 601: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 48
	i32 3641597786, ; 602: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 264
	i32 3643446276, ; 603: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 341
	i32 3643854240, ; 604: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 275
	i32 3645089577, ; 605: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3657292374, ; 606: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 192
	i32 3660523487, ; 607: System.Net.NetworkInformation => 0xda2f27df => 67
	i32 3672681054, ; 608: Mono.Android.dll => 0xdae8aa5e => 167
	i32 3676461095, ; 609: Xamarin.AndroidX.Camera.Lifecycle => 0xdb225827 => 238
	i32 3682565725, ; 610: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 235
	i32 3684561358, ; 611: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 243
	i32 3691870036, ; 612: Microsoft.AspNetCore.SignalR.Protocols.Json => 0xdc0d7754 => 181
	i32 3697841164, ; 613: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 346
	i32 3700866549, ; 614: System.Net.WebProxy.dll => 0xdc96bdf5 => 77
	i32 3706696989, ; 615: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 248
	i32 3716563718, ; 616: System.Runtime.Intrinsics => 0xdd864306 => 107
	i32 3718780102, ; 617: Xamarin.AndroidX.Annotation => 0xdda814c6 => 228
	i32 3724971120, ; 618: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 274
	i32 3732100267, ; 619: System.Net.NameResolution => 0xde7354ab => 66
	i32 3737834244, ; 620: System.Net.Http.Json.dll => 0xdecad304 => 62
	i32 3748608112, ; 621: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 216
	i32 3751444290, ; 622: System.Xml.XPath => 0xdf9a7f42 => 156
	i32 3751582913, ; 623: ZXing.Net.MAUI.Controls => 0xdf9c9cc1 => 312
	i32 3754567612, ; 624: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 215
	i32 3786282454, ; 625: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 241
	i32 3787005001, ; 626: Microsoft.AspNetCore.Connections.Abstractions => 0xe1b91c49 => 172
	i32 3792276235, ; 627: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3800979733, ; 628: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 204
	i32 3802395368, ; 629: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3819260425, ; 630: System.Net.WebProxy => 0xe3a54a09 => 77
	i32 3823082795, ; 631: System.Security.Cryptography.dll => 0xe3df9d2b => 125
	i32 3829621856, ; 632: System.Numerics.dll => 0xe4436460 => 82
	i32 3841636137, ; 633: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 194
	i32 3842894692, ; 634: ZXing.Net.MAUI => 0xe50deb64 => 311
	i32 3844307129, ; 635: System.Net.Mail.dll => 0xe52378b9 => 65
	i32 3849253459, ; 636: System.Runtime.InteropServices.dll => 0xe56ef253 => 106
	i32 3870376305, ; 637: System.Net.HttpListener.dll => 0xe6b14171 => 64
	i32 3873536506, ; 638: System.Security.Principal => 0xe6e179fa => 127
	i32 3875112723, ; 639: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 121
	i32 3876362041, ; 640: SQLite-net => 0xe70c9739 => 211
	i32 3885497537, ; 641: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 76
	i32 3885922214, ; 642: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 289
	i32 3888767677, ; 643: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 279
	i32 3889960447, ; 644: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 345
	i32 3893143201, ; 645: Microsoft.AspNetCore.Identity.EntityFrameworkCore => 0xe80ca6a1 => 177
	i32 3896106733, ; 646: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 647: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 247
	i32 3901907137, ; 648: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3920810846, ; 649: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 43
	i32 3921031405, ; 650: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 292
	i32 3928044579, ; 651: System.Xml.ReaderWriter => 0xea213423 => 152
	i32 3930554604, ; 652: System.Security.Principal.dll => 0xea4780ec => 127
	i32 3931092270, ; 653: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 277
	i32 3945713374, ; 654: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 655: System.Text.Encoding.CodePages => 0xebac8bfe => 132
	i32 3955647286, ; 656: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 231
	i32 3959773229, ; 657: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 266
	i32 3980434154, ; 658: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 340
	i32 3987592930, ; 659: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 322
	i32 4003436829, ; 660: System.Diagnostics.Process.dll => 0xee9f991d => 28
	i32 4015948917, ; 661: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 230
	i32 4017318820, ; 662: Microsoft.Bcl.TimeProvider.dll => 0xef736ba4 => 183
	i32 4023392905, ; 663: System.IO.Pipelines => 0xefd01a89 => 217
	i32 4025784931, ; 664: System.Memory => 0xeff49a63 => 61
	i32 4046471985, ; 665: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 206
	i32 4054681211, ; 666: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 89
	i32 4068434129, ; 667: System.Private.Xml.Linq.dll => 0xf27f60d1 => 86
	i32 4073602200, ; 668: System.Threading.dll => 0xf2ce3c98 => 144
	i32 4094352644, ; 669: Microsoft.Maui.Essentials.dll => 0xf40add04 => 208
	i32 4099507663, ; 670: System.Drawing.dll => 0xf45985cf => 35
	i32 4100113165, ; 671: System.Private.Uri => 0xf462c30d => 85
	i32 4101593132, ; 672: Xamarin.AndroidX.Emoji2 => 0xf479582c => 255
	i32 4101842092, ; 673: Microsoft.Extensions.Caching.Memory => 0xf47d24ac => 190
	i32 4102112229, ; 674: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 335
	i32 4125707920, ; 675: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 330
	i32 4126470640, ; 676: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 193
	i32 4127667938, ; 677: System.IO.FileSystem.Watcher => 0xf60736e2 => 49
	i32 4130442656, ; 678: System.AppContext => 0xf6318da0 => 6
	i32 4142654081, ; 679: Camera.MAUI.dll => 0xf6ebe281 => 169
	i32 4147896353, ; 680: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 89
	i32 4150914736, ; 681: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 342
	i32 4151237749, ; 682: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 683: System.Xml.XmlSerializer => 0xf7e95c85 => 158
	i32 4161255271, ; 684: System.Reflection.TypeExtensions => 0xf807b767 => 95
	i32 4164802419, ; 685: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 49
	i32 4181436372, ; 686: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 112
	i32 4182413190, ; 687: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 271
	i32 4185676441, ; 688: System.Security => 0xf97c5a99 => 129
	i32 4196529839, ; 689: System.Net.WebClient.dll => 0xfa21f6af => 75
	i32 4213026141, ; 690: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 216
	i32 4256097574, ; 691: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 248
	i32 4258378803, ; 692: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 270
	i32 4260525087, ; 693: System.Buffers => 0xfdf2741f => 7
	i32 4271975918, ; 694: Microsoft.Maui.Controls.dll => 0xfea12dee => 205
	i32 4274976490, ; 695: System.Runtime.Numerics => 0xfecef6ea => 109
	i32 4292120959, ; 696: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 271
	i32 4294763496 ; 697: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 257
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [698 x i32] [
	i32 67, ; 0
	i32 66, ; 1
	i32 107, ; 2
	i32 195, ; 3
	i32 267, ; 4
	i32 302, ; 5
	i32 47, ; 6
	i32 210, ; 7
	i32 79, ; 8
	i32 238, ; 9
	i32 141, ; 10
	i32 29, ; 11
	i32 346, ; 12
	i32 123, ; 13
	i32 209, ; 14
	i32 101, ; 15
	i32 173, ; 16
	i32 285, ; 17
	i32 106, ; 18
	i32 285, ; 19
	i32 221, ; 20
	i32 306, ; 21
	i32 76, ; 22
	i32 123, ; 23
	i32 13, ; 24
	i32 241, ; 25
	i32 131, ; 26
	i32 287, ; 27
	i32 147, ; 28
	i32 343, ; 29
	i32 344, ; 30
	i32 18, ; 31
	i32 235, ; 32
	i32 26, ; 33
	i32 175, ; 34
	i32 261, ; 35
	i32 1, ; 36
	i32 58, ; 37
	i32 41, ; 38
	i32 90, ; 39
	i32 244, ; 40
	i32 143, ; 41
	i32 263, ; 42
	i32 260, ; 43
	i32 315, ; 44
	i32 53, ; 45
	i32 68, ; 46
	i32 343, ; 47
	i32 226, ; 48
	i32 82, ; 49
	i32 328, ; 50
	i32 262, ; 51
	i32 214, ; 52
	i32 176, ; 53
	i32 327, ; 54
	i32 130, ; 55
	i32 54, ; 56
	i32 145, ; 57
	i32 73, ; 58
	i32 141, ; 59
	i32 61, ; 60
	i32 142, ; 61
	i32 348, ; 62
	i32 161, ; 63
	i32 339, ; 64
	i32 245, ; 65
	i32 12, ; 66
	i32 258, ; 67
	i32 124, ; 68
	i32 148, ; 69
	i32 180, ; 70
	i32 112, ; 71
	i32 162, ; 72
	i32 160, ; 73
	i32 260, ; 74
	i32 273, ; 75
	i32 83, ; 76
	i32 326, ; 77
	i32 320, ; 78
	i32 203, ; 79
	i32 146, ; 80
	i32 306, ; 81
	i32 59, ; 82
	i32 199, ; 83
	i32 50, ; 84
	i32 102, ; 85
	i32 113, ; 86
	i32 182, ; 87
	i32 39, ; 88
	i32 299, ; 89
	i32 296, ; 90
	i32 119, ; 91
	i32 334, ; 92
	i32 51, ; 93
	i32 43, ; 94
	i32 118, ; 95
	i32 250, ; 96
	i32 332, ; 97
	i32 256, ; 98
	i32 80, ; 99
	i32 219, ; 100
	i32 292, ; 101
	i32 233, ; 102
	i32 8, ; 103
	i32 72, ; 104
	i32 314, ; 105
	i32 151, ; 106
	i32 308, ; 107
	i32 150, ; 108
	i32 91, ; 109
	i32 303, ; 110
	i32 44, ; 111
	i32 329, ; 112
	i32 317, ; 113
	i32 307, ; 114
	i32 108, ; 115
	i32 128, ; 116
	i32 212, ; 117
	i32 25, ; 118
	i32 223, ; 119
	i32 71, ; 120
	i32 54, ; 121
	i32 45, ; 122
	i32 338, ; 123
	i32 298, ; 124
	i32 202, ; 125
	i32 251, ; 126
	i32 22, ; 127
	i32 265, ; 128
	i32 347, ; 129
	i32 85, ; 130
	i32 42, ; 131
	i32 156, ; 132
	i32 181, ; 133
	i32 70, ; 134
	i32 312, ; 135
	i32 278, ; 136
	i32 310, ; 137
	i32 3, ; 138
	i32 41, ; 139
	i32 62, ; 140
	i32 16, ; 141
	i32 52, ; 142
	i32 341, ; 143
	i32 302, ; 144
	i32 104, ; 145
	i32 210, ; 146
	i32 307, ; 147
	i32 300, ; 148
	i32 262, ; 149
	i32 33, ; 150
	i32 154, ; 151
	i32 84, ; 152
	i32 31, ; 153
	i32 12, ; 154
	i32 50, ; 155
	i32 55, ; 156
	i32 282, ; 157
	i32 35, ; 158
	i32 194, ; 159
	i32 316, ; 160
	i32 301, ; 161
	i32 231, ; 162
	i32 34, ; 163
	i32 57, ; 164
	i32 269, ; 165
	i32 176, ; 166
	i32 298, ; 167
	i32 171, ; 168
	i32 17, ; 169
	i32 304, ; 170
	i32 160, ; 171
	i32 329, ; 172
	i32 268, ; 173
	i32 201, ; 174
	i32 174, ; 175
	i32 295, ; 176
	i32 186, ; 177
	i32 218, ; 178
	i32 335, ; 179
	i32 149, ; 180
	i32 291, ; 181
	i32 169, ; 182
	i32 276, ; 183
	i32 186, ; 184
	i32 333, ; 185
	i32 233, ; 186
	i32 190, ; 187
	i32 28, ; 188
	i32 51, ; 189
	i32 179, ; 190
	i32 331, ; 191
	i32 296, ; 192
	i32 5, ; 193
	i32 315, ; 194
	i32 286, ; 195
	i32 290, ; 196
	i32 242, ; 197
	i32 308, ; 198
	i32 230, ; 199
	i32 213, ; 200
	i32 253, ; 201
	i32 84, ; 202
	i32 295, ; 203
	i32 60, ; 204
	i32 111, ; 205
	i32 56, ; 206
	i32 345, ; 207
	i32 282, ; 208
	i32 98, ; 209
	i32 19, ; 210
	i32 246, ; 211
	i32 110, ; 212
	i32 100, ; 213
	i32 172, ; 214
	i32 101, ; 215
	i32 313, ; 216
	i32 103, ; 217
	i32 300, ; 218
	i32 70, ; 219
	i32 37, ; 220
	i32 31, ; 221
	i32 102, ; 222
	i32 72, ; 223
	i32 319, ; 224
	i32 9, ; 225
	i32 122, ; 226
	i32 45, ; 227
	i32 232, ; 228
	i32 203, ; 229
	i32 9, ; 230
	i32 42, ; 231
	i32 4, ; 232
	i32 283, ; 233
	i32 184, ; 234
	i32 323, ; 235
	i32 318, ; 236
	i32 30, ; 237
	i32 135, ; 238
	i32 91, ; 239
	i32 92, ; 240
	i32 338, ; 241
	i32 48, ; 242
	i32 137, ; 243
	i32 111, ; 244
	i32 136, ; 245
	i32 252, ; 246
	i32 114, ; 247
	i32 301, ; 248
	i32 153, ; 249
	i32 75, ; 250
	i32 78, ; 251
	i32 272, ; 252
	i32 36, ; 253
	i32 294, ; 254
	i32 256, ; 255
	i32 249, ; 256
	i32 63, ; 257
	i32 135, ; 258
	i32 15, ; 259
	i32 115, ; 260
	i32 288, ; 261
	i32 297, ; 262
	i32 244, ; 263
	i32 47, ; 264
	i32 69, ; 265
	i32 79, ; 266
	i32 125, ; 267
	i32 184, ; 268
	i32 185, ; 269
	i32 93, ; 270
	i32 120, ; 271
	i32 305, ; 272
	i32 26, ; 273
	i32 214, ; 274
	i32 265, ; 275
	i32 96, ; 276
	i32 27, ; 277
	i32 240, ; 278
	i32 336, ; 279
	i32 0, ; 280
	i32 314, ; 281
	i32 145, ; 282
	i32 217, ; 283
	i32 165, ; 284
	i32 4, ; 285
	i32 97, ; 286
	i32 32, ; 287
	i32 92, ; 288
	i32 287, ; 289
	i32 199, ; 290
	i32 21, ; 291
	i32 40, ; 292
	i32 166, ; 293
	i32 330, ; 294
	i32 258, ; 295
	i32 322, ; 296
	i32 182, ; 297
	i32 272, ; 298
	i32 304, ; 299
	i32 297, ; 300
	i32 277, ; 301
	i32 2, ; 302
	i32 173, ; 303
	i32 133, ; 304
	i32 110, ; 305
	i32 200, ; 306
	i32 342, ; 307
	i32 223, ; 308
	i32 339, ; 309
	i32 57, ; 310
	i32 94, ; 311
	i32 321, ; 312
	i32 38, ; 313
	i32 234, ; 314
	i32 188, ; 315
	i32 25, ; 316
	i32 93, ; 317
	i32 88, ; 318
	i32 98, ; 319
	i32 10, ; 320
	i32 183, ; 321
	i32 86, ; 322
	i32 179, ; 323
	i32 99, ; 324
	i32 284, ; 325
	i32 180, ; 326
	i32 191, ; 327
	i32 218, ; 328
	i32 305, ; 329
	i32 225, ; 330
	i32 197, ; 331
	i32 177, ; 332
	i32 318, ; 333
	i32 7, ; 334
	i32 188, ; 335
	i32 269, ; 336
	i32 313, ; 337
	i32 222, ; 338
	i32 87, ; 339
	i32 264, ; 340
	i32 150, ; 341
	i32 317, ; 342
	i32 32, ; 343
	i32 115, ; 344
	i32 81, ; 345
	i32 215, ; 346
	i32 20, ; 347
	i32 11, ; 348
	i32 158, ; 349
	i32 3, ; 350
	i32 207, ; 351
	i32 325, ; 352
	i32 202, ; 353
	i32 200, ; 354
	i32 83, ; 355
	i32 195, ; 356
	i32 309, ; 357
	i32 63, ; 358
	i32 327, ; 359
	i32 291, ; 360
	i32 139, ; 361
	i32 170, ; 362
	i32 196, ; 363
	i32 273, ; 364
	i32 153, ; 365
	i32 185, ; 366
	i32 40, ; 367
	i32 116, ; 368
	i32 192, ; 369
	i32 224, ; 370
	i32 321, ; 371
	i32 198, ; 372
	i32 280, ; 373
	i32 130, ; 374
	i32 74, ; 375
	i32 65, ; 376
	i32 331, ; 377
	i32 168, ; 378
	i32 228, ; 379
	i32 178, ; 380
	i32 139, ; 381
	i32 105, ; 382
	i32 147, ; 383
	i32 69, ; 384
	i32 152, ; 385
	i32 191, ; 386
	i32 120, ; 387
	i32 126, ; 388
	i32 326, ; 389
	i32 148, ; 390
	i32 255, ; 391
	i32 198, ; 392
	i32 137, ; 393
	i32 242, ; 394
	i32 323, ; 395
	i32 20, ; 396
	i32 14, ; 397
	i32 134, ; 398
	i32 74, ; 399
	i32 58, ; 400
	i32 212, ; 401
	i32 245, ; 402
	i32 163, ; 403
	i32 164, ; 404
	i32 205, ; 405
	i32 15, ; 406
	i32 73, ; 407
	i32 6, ; 408
	i32 23, ; 409
	i32 267, ; 410
	i32 222, ; 411
	i32 90, ; 412
	i32 324, ; 413
	i32 1, ; 414
	i32 219, ; 415
	i32 268, ; 416
	i32 290, ; 417
	i32 133, ; 418
	i32 68, ; 419
	i32 142, ; 420
	i32 333, ; 421
	i32 309, ; 422
	i32 259, ; 423
	i32 201, ; 424
	i32 87, ; 425
	i32 95, ; 426
	i32 249, ; 427
	i32 254, ; 428
	i32 328, ; 429
	i32 30, ; 430
	i32 44, ; 431
	i32 263, ; 432
	i32 187, ; 433
	i32 196, ; 434
	i32 224, ; 435
	i32 108, ; 436
	i32 154, ; 437
	i32 34, ; 438
	i32 22, ; 439
	i32 113, ; 440
	i32 56, ; 441
	i32 288, ; 442
	i32 140, ; 443
	i32 117, ; 444
	i32 119, ; 445
	i32 109, ; 446
	i32 226, ; 447
	i32 221, ; 448
	i32 232, ; 449
	i32 53, ; 450
	i32 104, ; 451
	i32 334, ; 452
	i32 206, ; 453
	i32 207, ; 454
	i32 132, ; 455
	i32 303, ; 456
	i32 293, ; 457
	i32 281, ; 458
	i32 340, ; 459
	i32 259, ; 460
	i32 209, ; 461
	i32 155, ; 462
	i32 319, ; 463
	i32 246, ; 464
	i32 159, ; 465
	i32 131, ; 466
	i32 281, ; 467
	i32 157, ; 468
	i32 332, ; 469
	i32 270, ; 470
	i32 187, ; 471
	i32 136, ; 472
	i32 293, ; 473
	i32 289, ; 474
	i32 165, ; 475
	i32 208, ; 476
	i32 227, ; 477
	i32 299, ; 478
	i32 39, ; 479
	i32 175, ; 480
	i32 257, ; 481
	i32 80, ; 482
	i32 55, ; 483
	i32 36, ; 484
	i32 96, ; 485
	i32 162, ; 486
	i32 168, ; 487
	i32 294, ; 488
	i32 81, ; 489
	i32 229, ; 490
	i32 97, ; 491
	i32 29, ; 492
	i32 155, ; 493
	i32 18, ; 494
	i32 239, ; 495
	i32 126, ; 496
	i32 118, ; 497
	i32 253, ; 498
	i32 284, ; 499
	i32 237, ; 500
	i32 266, ; 501
	i32 239, ; 502
	i32 174, ; 503
	i32 286, ; 504
	i32 161, ; 505
	i32 261, ; 506
	i32 348, ; 507
	i32 237, ; 508
	i32 283, ; 509
	i32 274, ; 510
	i32 166, ; 511
	i32 16, ; 512
	i32 189, ; 513
	i32 140, ; 514
	i32 325, ; 515
	i32 124, ; 516
	i32 117, ; 517
	i32 37, ; 518
	i32 114, ; 519
	i32 46, ; 520
	i32 138, ; 521
	i32 116, ; 522
	i32 33, ; 523
	i32 171, ; 524
	i32 94, ; 525
	i32 52, ; 526
	i32 275, ; 527
	i32 0, ; 528
	i32 128, ; 529
	i32 149, ; 530
	i32 189, ; 531
	i32 24, ; 532
	i32 157, ; 533
	i32 252, ; 534
	i32 310, ; 535
	i32 144, ; 536
	i32 103, ; 537
	i32 88, ; 538
	i32 240, ; 539
	i32 59, ; 540
	i32 138, ; 541
	i32 99, ; 542
	i32 5, ; 543
	i32 13, ; 544
	i32 311, ; 545
	i32 211, ; 546
	i32 121, ; 547
	i32 134, ; 548
	i32 27, ; 549
	i32 320, ; 550
	i32 71, ; 551
	i32 250, ; 552
	i32 24, ; 553
	i32 170, ; 554
	i32 234, ; 555
	i32 279, ; 556
	i32 276, ; 557
	i32 337, ; 558
	i32 220, ; 559
	i32 213, ; 560
	i32 227, ; 561
	i32 247, ; 562
	i32 164, ; 563
	i32 280, ; 564
	i32 316, ; 565
	i32 100, ; 566
	i32 122, ; 567
	i32 251, ; 568
	i32 236, ; 569
	i32 236, ; 570
	i32 193, ; 571
	i32 159, ; 572
	i32 163, ; 573
	i32 254, ; 574
	i32 38, ; 575
	i32 204, ; 576
	i32 324, ; 577
	i32 178, ; 578
	i32 17, ; 579
	i32 167, ; 580
	i32 337, ; 581
	i32 336, ; 582
	i32 220, ; 583
	i32 146, ; 584
	i32 243, ; 585
	i32 151, ; 586
	i32 347, ; 587
	i32 129, ; 588
	i32 19, ; 589
	i32 64, ; 590
	i32 143, ; 591
	i32 46, ; 592
	i32 344, ; 593
	i32 225, ; 594
	i32 78, ; 595
	i32 60, ; 596
	i32 197, ; 597
	i32 105, ; 598
	i32 278, ; 599
	i32 229, ; 600
	i32 48, ; 601
	i32 264, ; 602
	i32 341, ; 603
	i32 275, ; 604
	i32 14, ; 605
	i32 192, ; 606
	i32 67, ; 607
	i32 167, ; 608
	i32 238, ; 609
	i32 235, ; 610
	i32 243, ; 611
	i32 181, ; 612
	i32 346, ; 613
	i32 77, ; 614
	i32 248, ; 615
	i32 107, ; 616
	i32 228, ; 617
	i32 274, ; 618
	i32 66, ; 619
	i32 62, ; 620
	i32 216, ; 621
	i32 156, ; 622
	i32 312, ; 623
	i32 215, ; 624
	i32 241, ; 625
	i32 172, ; 626
	i32 10, ; 627
	i32 204, ; 628
	i32 11, ; 629
	i32 77, ; 630
	i32 125, ; 631
	i32 82, ; 632
	i32 194, ; 633
	i32 311, ; 634
	i32 65, ; 635
	i32 106, ; 636
	i32 64, ; 637
	i32 127, ; 638
	i32 121, ; 639
	i32 211, ; 640
	i32 76, ; 641
	i32 289, ; 642
	i32 279, ; 643
	i32 345, ; 644
	i32 177, ; 645
	i32 8, ; 646
	i32 247, ; 647
	i32 2, ; 648
	i32 43, ; 649
	i32 292, ; 650
	i32 152, ; 651
	i32 127, ; 652
	i32 277, ; 653
	i32 23, ; 654
	i32 132, ; 655
	i32 231, ; 656
	i32 266, ; 657
	i32 340, ; 658
	i32 322, ; 659
	i32 28, ; 660
	i32 230, ; 661
	i32 183, ; 662
	i32 217, ; 663
	i32 61, ; 664
	i32 206, ; 665
	i32 89, ; 666
	i32 86, ; 667
	i32 144, ; 668
	i32 208, ; 669
	i32 35, ; 670
	i32 85, ; 671
	i32 255, ; 672
	i32 190, ; 673
	i32 335, ; 674
	i32 330, ; 675
	i32 193, ; 676
	i32 49, ; 677
	i32 6, ; 678
	i32 169, ; 679
	i32 89, ; 680
	i32 342, ; 681
	i32 21, ; 682
	i32 158, ; 683
	i32 95, ; 684
	i32 49, ; 685
	i32 112, ; 686
	i32 271, ; 687
	i32 129, ; 688
	i32 75, ; 689
	i32 216, ; 690
	i32 248, ; 691
	i32 270, ; 692
	i32 7, ; 693
	i32 205, ; 694
	i32 109, ; 695
	i32 271, ; 696
	i32 257 ; 697
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ a8cd27e430e55df3e3c1e3a43d35c11d9512a2db"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}
