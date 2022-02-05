[Back to main page](../README.md)

## Setting up the development environment

Many of these instructions may vary depending on your operating system. I'm writing this guide with the expectation that most people use Windows but please do ask for help if you are not and need help.

### Part 1 - Installing Git

1. Download Git from [here](https://git-scm.com/download).
2. Open the installer and follow its steps.
    - Note: When the select component steps come up and you are on Windows, I recommend that you leave the box ticked for the preselected items. Especially the Git Bash Here.

### Part 2 - Setting up SSH authentication with GitHub

> In this part, we will setup your SSH keys. This will make it easy for you to push (save) your changes to our project repository so that others can see them.

1. Follow the instructions for "Generating a new SSH key" and "Adding your SSH key to the ssh-agent" [here](https://docs.github.com/en/authentication/connecting-to-github-with-ssh/generating-a-new-ssh-key-and-adding-it-to-the-ssh-agent).
2. Follow the instructions [here](https://docs.github.com/en/authentication/connecting-to-github-with-ssh/adding-a-new-ssh-key-to-your-github-account) to add the generated SSH key to your GitHub account.

### Part 3 - Cloning the project

1. Open your file explorer and navigate to the directory where you want the project folder to be but don't create the folder.
2. Press Alt + D and type "CMD". This will open a command line window in your current path.
3. Now copy and paste this into the command line window: `git clone https://github.com/jeknom/buddy-up-jam-22.git`. You can close the command line.
4. You should now have the project folder in the path that you chose. You can close the command line window.

### Part 4 - Installing the correct Unity version

1. Download Unity Hub from [here](https://unity3d.com/get-unity/download).
2. Now install and open Unity Hub. You are likely going to be asked to create a Unity account if you do not have one already. So, create an account and sign in.
3. Select the Installs tab and click on the Install Editor button.
4. We are going to use the LTS version (2020.3.27f1) of Unity. LTS stands for long term support and is in most cases the most stable version of Unity. Click install next to it and follow the steps.
> The installer will ask if you wish to download some extra modules. If you are a coder it might be good to tick the box for Visual Studio if you do not have some other preferred IDE/editor.
5. Wait for the installation to finish.

### Part 5 - Opening the project in Unity

1. In Unity Hub, open the tab Projects and click on Open.
2. Find the project folder we cloned in part 3 and open it as the project folder.
3. This will open the project in Unity for you.