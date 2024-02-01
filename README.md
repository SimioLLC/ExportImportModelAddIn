# Export Import Model

This Add-In provides the ability to Export and Import a variety of Simio objects.
When the Add-In ("Export Import Model") is selected from the Simio desktop "Project Home" tab using the "Actions" button, a form is presented to the user.
This form has Export, Delete, and Import categories with options to Export or Import, including Table Data, Objects and Links, Vertices, Networks, Elements, etc.

To see this in action, you can create a source-server-sink and then use the Export-Import-Model to export "Objects and Links", then clear the model and then import "Objects and Links".

If you use the default Simio location for Simio desktop, you can simply clone this and run. If you don't use the default go to the Property "Debug" and set "Start external program" to the path to Simio.exe.

This was tested with VS2022 Community Edition.