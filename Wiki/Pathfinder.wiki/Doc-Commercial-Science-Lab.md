![](https://github.com/Angel-125/Pathfinder/wiki/DocSciLab.jpg)
**Mass:** 0.25t (unconfigured. Configured mass varies)  
**Crew Capacity:** 2  
**Cost:** 1,500  
**Research:** Advanced Exploration  
**Unlock Cost:** 4,000  
**Impact Tolerance:** 15 m/sec  
**Max Temp:** 900 K  
**Stored Volume:** 404.39 L  

The Doc Commercial Science Lab is designed to house the Science related functions of your base. It attaches to Pathfinder's standard crew ports included on all inhabitable modules such as the Chuckwagon, Hacienda, [[Ponderosa|Ponderosa]], and of course the Doc Commercial Science lab. The Doc can be stored in a standard KIS container, but it can also be surface attached to save storage space. The module is equipped with a special node called ImpactTransform to support drilling modules, though no templates with drills are currently available.

# Setup
As with any other part, simply grab the Doc out of your inventory and attach it to an available standard crew port.  
![](https://github.com/Angel-125/Pathfinder/wiki/DocSetup1.jpg)  
Once attached, you can right-click on the part to show its context menu, and press the Inflate button to inflate it.
![](https://github.com/Angel-125/Pathfinder/wiki/DocSetup2.jpg)  
The first time you inflate a Doc, you'll see the following hints window:  
![](https://github.com/Angel-125/Pathfinder/wiki/DocSciOpsView2.jpg)  
# Usage
To enter module, you have two options. First, you can click on another module's airlock, select an Astronaut, press the Transfer button, and click on the Doc. Second, you can EVA an Astronaut and walk up next to one of the Doc's airlocks.
![](https://github.com/Angel-125/Pathfinder/wiki/DocSetup3.jpg)  
## Managing Operations
Just like the Ponderosa, the Doc has an operations manager. Simply right-click on the part to display its context menu, and then click the Manage Operations button to bring up the operations manager. Below is a sample ops view:  
![](https://github.com/Angel-125/Pathfinder/wiki/DocSciOpsView0.jpg)  
**Command and Control (1) -** Press this to display the module's command and control buttons. See below for details.  
**Info (2) -** This view provides a description of the module's current configuration as well as the decal associated with the configuration.  
**Resources (3) -** Press this button to switch to the Resources view. See below for details.  
**Processors (4) -** This view shows the resource converters that are a part of the module, if any. To enable or disable a converter, simply click on the check mark next to the converter's name. See the page on Resource Production for details.  
**Reconfiguration Controls (5) -** These buttons let you reconfigure the module. You can switch between different configurations, preview their descriptions, and determine the resource cost required to reconfigure the module.  
***  
**Command and Control**  
![](https://github.com/Angel-125/Pathfinder/wiki/DocCnC.jpg)  
**Toggle Decals (1) -** Press this to hide or show the module's decals.  
**Toggle Lights (2) -** Press this to turn on or off the module's lights. Lights consume 0.04 ElectricCharge per second.  
***  
**Current Resources**  
![](https://github.com/Angel-125/Pathfinder/wiki/POM3.jpg)  
_The resources panel for the Doc is the same as for the Ponderosa._

The Resources tab displays the current and maximum resources that the module currently has. The contents of this window vary depending upon the module's current configuration.
## Reconfiguring
If you haven’t changed the settings in Pathfinder’s Settings Window, then you’ll need a Scientist to inflate/deflate the Doc as well as to reconfigure the module into a different configuration. Inflating the module or changing its configuration requires RocketParts, which represent the equipment and furnishings (cups, computers, desks, etc.) required by the module.  

In addition to reconfiguring the Doc in the field, you can reconfigure it in the VAB/SPH. Before launch, you can choose any configuration you want, and it won't cost you resources to do so.  

Below is a summary of the Doc’s configurations. For more information, click on the links provided.  
### Doc Commercial Science Lab  
Link: [[Doc Science lab|Doc-Science-Lab-Configuration]]  
Named after the module itself, the Doc Commercial Science Lab provides similar functionality to the stock Mobile Processing Lab. The Doc is best suited for ground-based operations and isn't particularly good in orbit. But unlike the stock lab, the Doc lets you take your researched Science and convert it to Reputation and Funds.  
### Sunburn Fusion Lab  
Link: [[Sunburn Fusion Lab|Sunburn-Fusion-Lab]]  
This lab is suited for making FusionPellets for fusion reactors, and Coolant for heat radiators.  
### Watney Chemistry Lab  
Link: [[Watney Chemistry Lab|Watney-Chemistry-Lab]]
Available only in Advanced Mode, the Watney offers several chemical converters to support your base's resource needs.
# Designer's Notes  
When KSP 1.0 was released, the Mobile Processing Lab gained the ability to take experiment data and perform research on it to generate more Science. It takes awhile, but once completed, you can transmit it back to KSC. It's an interesting mechanic that gives the MPL relevance, but I also think Squad came up a bit short, because the game has three currencies: Science, Reputation, and Funds. Why not provide the ability to increase all three? Combine that with players asking me for a mobile processing lab equivalent for Pathfinder, and I knew I had an opportunity with the Doc Science Lab. 

I started thinking about what would generate Reputation, and hit upon the idea that publishing your research would impress your peers. Similarly, selling your research would give you Funds. Off camera, some other agency would make use of your data. Originally I wanted the player to lose some Reputation during the sale to represent your peers getting mad with you literally selling out, but I decided that just wouldn't do, as it would discourage players from selling.

It took quite a bit of trial and error, but the Commercial Science Lab is the result. It also set the stage for me to play with the game's currencies. How do you reduce Reputation? In addition to not fulfilling your contracts, declare your core samples as invalid, or install Snacks and stop feeding your Astronauts. How do you reduce Science? Perform biome analyses in the Pathfinder Geology Lab. How do you reduce Funds? Provide a useful service that costs money. And how do you convert between currencies? Use the Doc Science Lab, of course. :)