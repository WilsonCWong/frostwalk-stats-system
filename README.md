# Frostwalk Stats System
This is a simple stats system with a custom inspector-based design tool to help you easily create stats for your game characters and entities. As of v1.1, it also comes with a redesigned event system based on Ryan Hipple's ScriptableObject approach. I've taken inspiration from [NeoDragonCP](https://github.com/NeoDragonCP/Unity-ScriptableObjects-Game-Events-) and [Sipstaff](https://github.com/Sipstaff/Unity-SO-Events-With-Data)'s variation on this system.

<p align="center"> 
<img src="https://i.imgur.com/el5Pqxn.png" title="Stats Inspector" style="width: 60%;" />
</p>

## Requirements
* Unity 2017.4.17f1 or above
* Odin Inspector and Serializer 2.0.13 or above ([Purchase here](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041))

To install, grab the latest package release and import it into your project. If you haven't already, also import Odin Inspector and Serializer into your project.

### Why is Odin required?
Odin allows for the serialization of dictionaries, which I make use of in the stat system. Due to this, I'm able to easily create an editor tool to modify the stats. I could've hacked it to use a list instead, but the tool wouldn't be as functional or look as good and the API wouldn't be as clean.

## How does it work?
The stats are based on points. Many games come to mind: Elder Scrolls, Fallout, Mount & Blade, FTL, etc. There is also an option to have these points level up based on experience.

To start using, create a Stats Object in your assets. This is done by right clicking in your Project Pane and going to Create -> Skill System -> Stat Object. This creates a Stat asset that can be plugged into your characters directly or serve as a template as part of a Factory pattern (which the example in the project kind of does).

Refer to the example Player.cs and StatSystem.cs under the Examples/Test folder. The example shows the usage of event handlers to get information from a stat and how you would go about using this system. You can test the functionality by double clicking the stats property on the Player object and using the inspector tools provided.

I will go back later to perform some more code cleanup and extend the API a bit for StatSystem.cs to make event subscriptions easier.

## Is this plug and play or ready for production?
No. The event system is not contained in its own namespace and the code could use some documenting. This was just a test project I've done in preparation for developing my next game. It is, however, a good foundation to build off of to get up and running.
