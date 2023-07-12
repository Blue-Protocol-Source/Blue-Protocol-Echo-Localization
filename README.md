# Blue Protocol Echo Localization

This tool provides the ability to apply translations to server sent data for Blue Protocol.

Two methods of localization can be used: Complete Replacement or Delta Replacement

## Complete Replacement

This is the standard system where a user provides a completely new localization json file for their client to load.

## Delta Replacement

With this system, only the modified strings (and their respective identifiers) needs to be provided. However, it does come with the requirement of needing an AES Decryption key for the delta merging to be possible. This method is the most ideal for always having your client display the latest game strings while still including your incomplete localizations. So even when a patch happens, you don't need to wait on a new localization database being created to enjoy the game in your desired text language.

## How It Works

Echo Localization serves as a self local hosted proxy server on your machine and the game is directed to pass through it instead of making HTTPS calls directly to the server endpoints.

To redirect the client, the `%LOCALAPPDATA%\BLUEPROTOCOL\Saved\Config\WindowsClient\Game.ini` file is modified with the following contents:
```ini
[/Script/SkyBlue.SBHttpQuery]
RequestScheme=http
RequestPort=9192
RequestHost=localhost
MasterDataBaseURL=localhost
```

**Important Note:** Due to this change, Echo Localization must remain running at all times while the game client is running. Otherwise calls to the server will fail and your experience will be degraded and ultimately disconnected from your session.

## Getting Started

A few pieces of setup are required before running this tool.

First and potentially most important, if you are running with a **VPN in Split Tunnel mode**, you will want to add Echo Localization to the included programs on the list to use the VPN connection. For users on a VPN that is using a Full Tunnel (all applications going through the VPN with no exlcusions) there is nothing you need to specifically do for your VPN to work with the tool.
* There is a `Check IP` button included on the UI to allow you to verify if you have correctly passed Echo Localization through your VPN before running the game.

Next, regardless of the Replacement method used, you will need to have a localization json file locally on your machine to point the tool to.

This directory should be structured in the following way, starting from the root folder where Echo Localization is stored (though any directory can be used if desired)
```
Echo Localization.exe
Overrides\Localizations\en\loc.json
```
1. Once the application is running, simply select the `Overrides` directory for the `Override Data Directory` setting.
2. The Localization dropdown will be updated to reflect any folders that are in the `Localizations` subdirectory.
3. Select the Localization you want to apply to the game, by default none will be applied and you will play in the default game language.
4. Launch the game and Echo Localization will take care of the rest.
5. As the tool sees requests and handles them, they will appear in the Debug Log section.

Important Note: If you are intending to use **Delta Replacement** you will need to check the `Show Advanced Settings` checkbox and then enter in the AES Decryption Key for the client's web requests.
* This key is _different_ from the PAK file decryption key

### Additional Advanced Functions

For users that are interesting in inspecting game data, there is the ability to save out web requests directly to files. Any request can have its data saved, but only if an AES Decryption key is provided can the data be written to a file in a decrypted state.

The recommended setup for saving data is to create a new folder in the same directory as Echo Localization and name is ServerData
```
Echo Localization.exe
ServerData\
```
1. Once the application is running, select the `ServerData` folder for the `Saved Data Directory `setting.
2. Check the `Save Incoming Data To File` checkbox to have future web requests saved to a file.
3. If an AES Decryption Key has been provided, in the same `Advanced Settings` area, the `Decrypt Saved Data` checkbox can be enabled for the written data to be in its decrypted format.

Note: When saving requests, they are stored automatically in two ways:
1. Versioned directories
2. Latest directory

As the names suggest, any request saved that has not been saved before, is written into the `ServerData\<path>\ServerFileTimestamp\<File.ext>` directory format. This allows for building an archive of all potential files downloaded from the server for later reference. Additionally, ALL requests being saved are written into the `ServerData\latest\<path>\<File.ext>` directory so you have a mirror of the latest data received by your client from the server in an easy to browse location.
Example output directory structure:
```
ServerData\apiext\achievements\2023_07_05-03_48_07\achievements.json
ServerData\apiext\achievements\2023_07_12-04_33_56\achievements.json
ServerData\apiext\texts\ja_JP\2023_07_05-05_56_09\ja_JP.json
ServerData\apiext\texts\ja_JP\2023_07_12-04_33_55\ja_JP.json
ServerData\latest\apiext\texts\ja_JP.json
ServerData\latest\apiext\achievements.json
ServerData\latest\apiext\emotes.json
```
