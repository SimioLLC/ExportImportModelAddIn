# Export Import Model

This Add-In provides the ability to export, import and delete a variety of Simio objects, links, table data, etc... from your model.  This add-in provides a quick way modify your model.  It is also a great resource for understanding the Simio Design Time API.

The Add-In ("Export Import Model") is selected from the Simio desktop "Project Home" tab using the "Actions" button.  When selected, a form is presented to the user.  This form has Export, Delete, and Import categories with options to Export or Import, including Table Data, Objects and Links, Vertices, Networks, Elements, etc.

To see this in action, you can create a source-server-sink and then use the Export-Import-Model to export "Objects and Links", then clear the model and then import "Objects and Links".

If you use the default Simio location for Simio desktop, you can simply clone this and run. If you don't use the default go to the Property "Debug" and set "Start external program" to the path to Simio.exe.

This was tested with VS2022 Community Edition.

![image](https://github.com/SimioLLC/ExportImportModelAddIn/assets/42541127/5ceaadda-f0df-4f22-9a09-803f91a33ef4)

Here are some quick instructions on how to setup this add-in.

1) Download the "ExportImportModelAddIn.dll"

2) Right-click the assembly and select properties. Then choose to "unblock" button and press apply. (only needed for Windows 7 and later).

3) Copy "ExportImportModelAddIn.dll" into "C:\Users\<YourUserName>\Documents\SimioUserExtensions". You might need to add the SimioUserExtensions folder under MyDocuments (C:\Users\<YourUserName>\Documents) if it does not already exist.  (e.g. C:\Users\GlenWirth\Documents\SimioUserExtensions).

5) Open Simio and load your model.

6) From the Project Home...Select Add-In button, select "Export Import Model"

7) Choose the function that you want to run (e.g. Export, Delete, Import, etc..). 
