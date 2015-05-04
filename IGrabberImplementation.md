#How to create Grabber plugin

### General ###
To create new grabber for the program, all you need to do is to implement IGrabber interface (in XMLTVGrabber.dll), build your project as DLL, add it to Grabbers folder in XMLTVGrabber installation folder and restart the program.

### Details ###
IGrabber interface contains these methods:
  * `void Initialize();`
  * `List<GrabberChannel> GetChannels();`
  * `List<TvProgram> GetPrograms(object[] args, DateTime date);`
and this event:
  * `event Logger.OnLogEvtArgs LogMessage;`

Use Initialize to initialize you variables. Think of it like your constructor.

GetChannels is used to get the updated channel list from the website used to scrape the data.

GetPrograms is used to get the programs for a specific date. The args is the arguments you set when you returned the channels list.

LogMessage event is used to show the message in the log window and the log file. OnLogEvtArgs contains the log message and the message type (Info, Warning, Error, Debug).