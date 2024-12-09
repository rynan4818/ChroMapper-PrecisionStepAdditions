# ChroMapper-PrecisionStepAdditions

BeatSaberの作譜ツールの[ChroMapper](https://github.com/Caeden117/ChroMapper)で、このプラグインを使うとカーソル移動単位の枠を3～5個の範囲で拡張することができます。

このプラグインは[BeatSaber AdventCalendar 2024](https://adventar.org/calendars/9970)の10日目に以下の記事で、ChroMapperのプラグイン作成方法を紹介するために作成されました。

**[ChroMapperプラグインの創りかた](https://note.com/rynan/n/nb1aa807bea9d)**

記事で説明したものから、追加する数を選択できるように拡張されています。

---
Using this plugin with the BeatSaber score editor [ChroMapper](https://github.com/Caeden117/ChroMapper), you can expand the cursor movement unit frame by 3 to 5 units.

This plugin was created on the 10th day of the [BeatSaber AdventCalendar 2024](https://adventar.org/calendars/9970) to introduce how to create a ChroMapper plugin in the following article.

**[How to make a ChroMapper plugin](https://note.com/rynan/n/nb1aa807bea9d)** (The article is written in Japanese.)

It has been expanded so that you can choose the number to add from the ones explained in the article.

![image](https://github.com/user-attachments/assets/5c7e4275-7f15-4a3c-b8d1-cd7be479ec4e)

![image](https://github.com/user-attachments/assets/4e4b96ec-2e6f-4da7-809f-5a4f70d142ed)

# インストール方法 (How to Install)

1. [リリースページ](https://github.com/rynan4818/ChroMapper-PrecisionStepAdditions/releases)から、最新のプラグインのzipファイルをダウンロードして下さい。

    Download the latest plug-in zip file from the [release page](https://github.com/rynan4818/ChroMapper-PrecisionStepAdditions/releases).

2. ダウンロードしたzipファイルを解凍してChroMapperのインストールフォルダにある`Plugins`フォルダに`ChroMapper-PrecisionStepAdditions.dll`をコピーします。

    Unzip the downloaded zip file and copy `ChroMapper-PrecisionStepAdditions.dll` to the `Plugins` folder in the ChroMapper installation folder.

3. インストールすればカーソル移動単位は3個に拡張されます。4～5個にしたい場合は、以下の設定ファイルの`additionalStep`を直接編集して下さい。

    If you install it, the Cursor Interval will be expanded to 3. If you want to make it 4 or 5, please edit the `additionalStep` in the following configuration file directly.

# 設定ファイルについて (About the configuration file)
設定ファイルはChroMapperの設定ファイルと同じフォルダ`ユーザ設定フォルダ(Users)\ユーザ名\AppData\LocalLow\BinaryElement\ChroMapper`の`PrecisionStepAdditions.json`に保存されます。

The configuration file is saved in `PrecisionStepAdditions.json` in the same folder as ChroMapper's configuration file `User Settings Folder(Users)\User Name\AppData\LocalLow\BinaryElement\ChroMapper`.

| 設定項目 (Setting Item) | デフォルト値 (Default Value) | 説明 (Description) |
|:---|:---|:---|
| additionalStep | 1 | 追加する移動単位の数(0～3)<br>Number of Cursor Intervals to add (0～3)|
| cursorPrecisionC | 16 | 3個目の移動単位の分母<br>Third cursor interval precision |
| cursorPrecisionD | 24 | 4個目の移動単位の分母<br>Fourth cursor interval precision |
| cursorPrecisionE | 32 | 5個目の移動単位の分母<br>Fifth cursor interval precision |

# 開発者情報 (Developers)
このプロジェクトをビルドするには、ChroMapperのインストールパスを指定する`ChroMapper-PrecisionStepAdditions\ChroMapper-PrecisionStepAdditions.csproj.user`ファイルを作成する必要があります。

To build this project, you must create a `ChroMapper-PrecisionStepAdditions\ChroMapper-PrecisionStepAdditions.csproj.user` file that specifies the ChroMapper installation path.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <ChroMapperDir>C:\TOOL\ChroMapper\chromapper</ChroMapperDir>
  </PropertyGroup>
</Project>
```
