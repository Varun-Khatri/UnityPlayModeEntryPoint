# UnityPlayModeEntryPoint

**UnityPlayModeEntryPoint** is a lightweight editor utility that solves the common "play from wrong scene" problem. Instead of manually switching to your Splash or Main Menu scene every time you want to test your game, this tool allows you to set a persistent "entry point" scene.

## 🚀 Features
- **Force Start Scene:** Always enter Play Mode from a specific scene.
- **Persistent Settings:** Uses `EditorPrefs` and `[InitializeOnLoad]` to remember your choice even after restarting Unity.
- **Toggleable:** Quickly enable or disable the forced start without losing your scene selection.
- **Production Ready:** Includes an Assembly Definition (`.asmdef`) to ensure zero impact on your final game builds.

## 🛠 Installation

### Via Git URL (Recommended)
1. Open the Unity Project you want to use the package in.
2. Open the **Package Manager** window (`Window` > `Package Manager`).
3. Click the **+** (plus) icon in the status bar.
4. Select **Add package from git URL...**.
5. Paste the following URL:
   `https://github.com/Varun-Khatri/UnityPlayModeEntryPoint.git`
6. Click **Add**.

## 📖 How to Use
1. Go to **Window > General > Play Mode Start Scene**.
2. Check the **Enable Forced Start Scene** box.
3. Drag your desired start scene (e.g., `MainMenu.unity`) into the **Target Scene** slot.
4. Hit **Play**! Unity will now automatically load that scene first, no matter what you are currently working on.

## ⚙️ Technical Details
- **Unity Version:** 2019.4 LTS or higher.
- **Logic:** Uses `EditorSceneManager.playModeStartScene` which is the official Unity API for this behavior.
- **Builds:** Because the script is located in an `Editor` folder with a specific platform-targeted Assembly Definition, it is completely stripped from your final build.

## 📄 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

