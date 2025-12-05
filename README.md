# BackupWizard

Just a simple backup wizard I wrote using C#, simple and straightforward.
What it basically do is take a backup to your desired folder and whether a file inside is opened, it will still make a backup.
Reason for this is that the logic flow will copy first a temp folder from the target folder and then zip that temp folder.
So basically we didn't touch the target folder. 
Downside is that as the target folder's size gets bigger, the time it'll take the process takes longer too due to the same reaason above.
