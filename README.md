# DreamStorm Debugging Tools
<i>DreamStorm Debugging Tools</i> is package which allows you to customize debug logging in your Unity project. It lets you categorize logs and configure when and where those logs are sent. You can also create custom object to log conversion.
## Configuration
For tool setup we created special Editor Window. You can find it in <i>Tools / DreamStorm / Debugger Configuration</i>. Inside you will find log sending configuration, log categories configuration and log categories creator.
## Custom Object Conversion
You can convert objects to logs by implementing IObjectConverter. This allows for simle formating of event the most complex objects.
## Usage
You need to invoke any API from Debugger class.
Our API follows Unity debug log convention, so you can also replace Unity Debug.Log variants with using Debug = Debugger.Log variant.
## License
[MIT](https://github.com/dreamstormstudios/Debugging-Tools/blob/main/LICENSE.md)
