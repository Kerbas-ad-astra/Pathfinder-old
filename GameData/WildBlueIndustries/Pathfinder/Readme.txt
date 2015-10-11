Pathfinder

A KSP mod that blazes the trail for more permanent installations. Geoscience for better resource extraction.

---INSTALLATION---

Copy the contents of the mod's GameData directory into your GameData folder.

---REVISION HISTORY---

0.8.6

This release fixes a bunch of issues that cropped up in Pathfinder as well as the Buffalo. It also introduces new game mechanics for the Hot Springs geothermal plant- thanks for your input, AdmiralTigerclaw! :)

New Parts
- Added MC-2000 Buckboard and MC-3000 Buckboard. With colliders and glowing resource decals. ;)
- Added the Auxiliary Electronic Navigator (AuxEN). In the Old West, oxen were frequently used to pull wagons. The AuxEN doesn't pull anything, but it does the driving. The probe core is the same size as a Chassis 1u, and supports RemoteTech, AntennaRange, and kOS.
- Added an adapter that tapers from the Buffalo cab form factor to a 1.25m cylinder. 

Buffalo Command cab
- Widened the headlight beams and angled them down slightly for better ground operations.
- You can now toggle the headlights separately from the cabin lights. Both cabin lights and headlights will turn on when you tap on the Lights button.

Buffalo Crew Cab
- Reworked the crew cab to include doors and ladders on its sides.
- Added a small amount of inventory space.

Doc
- Added an ImpactTransform node to the 3D mesh to accomodate drills.

Ponderosa
- Renamed the radially attached Ponderosa to the Casa.
- Increased empty mass of the Ponderosa to 0.35t to reflect its contents.
- Added an ImpactTransform node to the 3D mesh to accomodate drills.

Hacienda
- Increased empty mass of the Hacienda to 0.5t.
- Added an ImpactTransform node to the 3D mesh to accomodate drills.

Clockworks
- Maximum storage will increase to 10k liters when you convert the Hacienda to become a Clockworks. This matches the maximum volume part that you can 3D print.

Fireworks
- Really added EL productivity to the Fireworks, and got rid of ExSurveyStation. There was a mixup when preparing the last release...

Brew Works
- Brought the Autobots in and transformed Xenon into XenonGas.

Hot Springs
- Refactored the power planet to require Water and GeoEnergy as resources before it can operate. The HotSprings has a dedicated "drill" to extract GeoEnergy, called the Geothermal Tap. Right-click on the Hacienda to use the Geothermal Tap. You can also drill for GeoEnergy using the Gold Digger and stock Drill-O-Matic. Simply right-click on the drill and modify the resource that it drills for. Not all planets are ideal for finding GeoEnergy. Check out GeoEnergyResource.cfg to see how this initial pass is set up. Below is a quick rundown:

Geologically Active Worlds
Eve
Kerbin (duh)
Laythe
Eeloo (!) See See http://io9.com/breaking-geologic-activity-has-been-detected-on-the-su-1718055390

Possibly Geologically Active. (% chance per save)
Moho (70%)
Mun (85%) See http://www.space.com/14632-moon-dead-geologic-activity-monitored.html
Duna (70%) See http://astrogeo.oxfordjournals.org/content/44/4/4.16.full
Ike (70%)
Val (70%)
Tylo (60%)

Not geologically active
Sun
Gilly
Minmus - wiki suggests that it's a captured comet
Jool
Bop
Pol
Dres

If a world is not listed, then it has a base 70% chance of having GeoEnergy.

Bug Fixes
- The Switchback's nodes are now in their correct places.
- The Buckboard MC-1000's decal glows again.
- Fixed file paths for several Buffalo parts so they'll show up again.
- Moved Trailer Hitch to Utility.
- Moved Chassis Decoupler to Structural.

0.8.5
- Fixes issues with the Buffalo. When KSP 1.0.5 comes out, the Buffalo will not be included in Pathfinder.

0.8.4
- Hotfix to add collider to the Buckboard.

0.8.3

This update brings with it some more bug fixes, play balance, and some new parts. The existing Ponderosa now has a near twin that is radially attached just like the Hacienda and Doc. Originally I wanted the Ponderosa to work this way, but couldn't figure out how to make the airlocks work properly. Now that I do know how to make them work, it might be time to retire the containerized Ponderosa. Anyway, since a radially attached Ponderosa needs something to attach to, I also created the Switchback, a 4-way base hub that also has a small amount of storage and a solar panel on its roof. Simply attach a Saddle to the ground and place the Switchback onto the Saddle. Due to some constraints on placing the crew ports so they line up with the inflatable modules, the Switchback does not inflate. Instead, it's assembled off-camera when you pull it from storage. Finally, to resolve an issue with Extraplanetary Launchpads, Pathfinder introduces the Spyglass Survey Module. The Spyglass provides a spot to monitor your vessel construction efforts.

Buffalo
The Buffalo is now located in its own dedicated directory under WildBlueIndustries, and has its own category in the parts catalog. While it continues to be bundled with Pathfinder for the time being, the Buffalo MSEV is now available separately as its own mod. You can download it here.

Buckboard
- Gave the MC-1000 Buckboard a facelift; it is also now a common asset for WildBlueTools, which means it's no longer in the Pathfinder category. You can find it under Utilities.

New Parts
- By popular request, added the radially attached Ponderosa. It works just like the existing containerized Ponderosa, but it is radially attached like the Doc and Hacienda.

- Added the Switchback 4-way hub. Unlike other base components, the Switchback is assembled off-camera once the kerbal pulls it out of storage.

- Added the Spyglass Survey Module. Right-click the part to show the Extrplanetary Launchpads UI. Yes, I do have plans to make the light rotate.

Templates
- Reduced output of the greenhouse to feed 2 kerbals for about 90 days. It just wasn't as fun with the greater yield, even though the math does support the higher output.
- Added ExWorkshop module to the Fireworks to improve base productivity, and removed ExSurveyStation since it wasn't responding well to being loaded dynamically.

Bug Fixes
- The Hacienda Operations Manager now correctly labels its operations manager as Hacienda Operations.
- Fixed an issue Where efficiency and extraction monitors were creating NREs while in solar orbit.
- Fixed an issue where the Gold Digger was creating NREs while in solar orbit.
- Fixed an issue where the hints window would keep showing up even though Pathfinder had already shown the hint.
- Fixed an issue where the Extraplanetary Launchpads window wouldn't show up (see Spyglass for the solution).

Other
- By popular request, added support for USI-LS. Thanks for the patch, badsector! :)

0.8.1: William Tell Overture

Buffalo

- Added ElectricCharge to the Buffalo Crew Cab and Wagon.
- The Buffalo Wagon now properly shows up in the Utility tab.

Module Manager Patches

- Added Antenna Range Module Manager patch by badsector. Thanks badsector & linuxgurugamer! :)

Bug Fixes

- The Recycle ScrapMetal converter (which requires Extraplanetary Launchpads) now requires 9.75 units of ScrapMetal to produce 1 unit of Metal. This accounts for conservation of mass and for density differences between Metal and ScrapMetal.

- Fixed an issue in the commercial science lab where it was incorrectly converting the lab's data into Funds and Reputation instead of its stored Science.
- Fixed an issue in the commercial science lab where it would remove the stored science before completing data transmission.
- The commercial science lab will only show the Publish Research and Sell Research buttons when in Career Mode.

0.8.0: Pathfinder Beta: Home On The Range

NOTE: This is a beta release, there are bound to be bugs.

This release Introduces the Buffalo Modular Space Exploration Vehicle (MSEV). It is based upon NASA's real-world Multi-mission Modular Space Exploration vehicle (MMSEV). Just like the NASA vehicle, the Buffalo can be configured as a rover, but it doesn't stop there. (Coming Soon!) The Buffalo can become an asteroid exploration craft, a munar lander, a cargo hauler, and more. This initial offering provides the components needed to build the "space truck" version of the Buffalo; future versions will offer in-space functionality. The best thing is that the Buffalo can be assembled by an engineer via KIS, and the vehicle is sized to fit into a standard 3.75m cargo bay!

The Buffalo components include:

Buffalo Command Cab: A command pod that seats two kerbals, with Stock IVA and ASET Props/Avionics-based IVA (highly recommended). It also has integrated ladders.
Buffalo Crew Cab: A crew cabin that seats two kerbals, also with IVA. It also has a rooftop solar panel.
Buffalo Wagon: A.K.A. the Wagon, this Buffalo Storage Container holds lots of resources when deployed. It is a semi-rigid container that is shaped like the Crew Cab, and it too has solar panels on its roof.
Chassis (1u): The chassis serves as the foundation for the Buffalo. It is sized to the same length and width of a KIS standard SM-62 storage container/Buckboard/Ponderosa. While it is initialy configured as a battery, the Chassis can store a variety of different resources once converted to storage.
Chassis (2u): Same as above, but it's twice as long.
Standard Flatbed (1u): Sized to be as wide as the Buffalo cabin, the flatbed is sized to hold 2 SM-62 containers/Buckboards/Ponderosas. While it takes an engineer to mount the flatbed onto a chassis, anybody can put cargo on the flatbed.
Standard Flatbed (2u): Same as above, but twice as long.
Wide Flatbed (1u): A wider version than the standard flatbed, the wide flatbed can hold 3 SM-62 containers/Buckboards/Ponderosas. It is sized to fit in a standard 3.75m cargo bay.
Wide Flatbed (2u): Same as above, but twice as long.
Trailer Hitch: This specialized "docking port" is used to connect trucks and trailers together. It also has an integrated ladder.
Chassis Decoupler: For those times when you need to mount a Buffalo to a rocket, the chassis decoupler fits the bill.
Interim Wheel: Based on the RoveMate M1, the Interim Wheel provides ground transportation while we wait for KSP 1.1 and its better wheel system.

Bug Fixes
- Fixed an issue with the stack node on the Mineshaft. Now you can use it to close the gap between Buffalo modules separated by trailer hitches!

---ACKNOWLEDGEMENTS

Eve: Order Zero graphic courtesy of Kuzztler and used with permission.

0.7.8

Templates
- Added LqdHydrogen/Oxidizer converter to the Watney.
- Added support for Connected Living Spaces.

Bug Fixes
- Fixed an issue where adding the commercial science lab could cause an issue if the part did not have a science lab. Thanks kerbas_ad_astra!
- Fixed an issue where the agency flag wasn't showing up.

0.7.7: The Last of the Mohicans - Top of The World
Another bug fix, this release fixes a couple of issues with lights and mesh switching and animation that occurred when you reverted back to launch. It also fixes an issue where you'd lose your Snacks when switching templates- if you have the Snacks mod installed. It also adds a new template to the Hacienda.

Templates
- Added the Brew Works ISRU Refinery to the Hacienda. Like the stock ISRU part, the Brew Works converts Ore into LFO, LiquidFuel, Oxidizer, and MonoPropellant. It also can refine Xenon.

Bug Fixes
- Fixed an issue in WBIAnimation and WBILight that would lose their settings when you reverted to launch.
- Fixed an issue where, if you have Snacks! installed and you reconfigure your modules, the modules would lose their Snacks. Kerbals should be happier now.

0.7.6: The Last of the Mohicans - Elk Hunt

This release brings with it some bug fixes (thanks ozraven!) as well as a new part: the Poncho! Think of buying 10 OX-STAT panels and gluing them together, except that they fold out and cost as much as 15 OX-STAT panels. It won't track the sun, but the Poncho will provide a lot of power for your fledgling base. The Sombrero circular solar array is still planned...

New Part
- Added the Poncho, a large folding solar array that cannot track the sun. But hey, you can mount it on the Buckboard, Ponderosa, Saddle, and SM-62.

Bug Fixes
- Fixed an issue where the drill switcher would show an empty window if the biome hasn't been unlocked yet.

Special thanks to ozraven:
- Fixes for reconfigure affordability check.
- Deflating discards resources that cannot be stored.
- Entire vessel's crew now searched for skill modifier calculation.
- Updated inflate/deflate logic to account for deflate confirmation.

0.7.5 The Last of the Mohicans

This update brings the last of Pathfinder's core base modules. Specifically, it updates the Chuckwagon and it adds optional life support templates that will appear if you have a life support mod installed. Pathfinder supports TAC Life Support (the gold standard of life support), and Snacks out of the box. Other life support options are possible; just read up on how to edit a template file on the wiki. You may also want to check out the MM_TACLS.cfg and MM_Snacks.cfg files for examples. Additionally, you can now generate power using the Hot Springs Geothermal Plant. Keep in mind that it's maintennance intensive, a bit finicky, and not 100% efficient on all worlds. You also have the Solar Flare Experimental Fusion Plant and it's also a bit ornery. Together the Hot Springs and Solar Flare represent the upper end of Pathfinder's power options- lower-tier options are in the works. Finaly, the Ponderosa Habitat is now more than just a placeholder... if you have kOS installed.

Chuckwagon
- You can now science the kraken out of the Chuckwagon and convert it into a greenhouse.
- Added front and side attachment points.
- Added temporary IVA. IVAs are on backorder.
- Added airlocks.

Templates
- You can now retool the Blacksmith to use RocketParts instead of MaterialKits, just like the Clockworks.
- More than just a habitat module, the Ponderosa template is also used as a command and control center. The template now provides support for kOS if you have that mod installed and if you've researched Precision Engineering. kOS is great for creating power management scripts; it works best with Action Groups Extended.
- Added the Pigpen Recycler to the Ponderosa. With a life support mod, it has some useful recyclers.
- The Geology Lab now shows the current efficiency improvements for Science, Industry, and Habitation- based resource processors for the current biome.
- Added the Hot Springs Geothermal Plant to the Hacienda. Hot Springs generates lots of power but it is maintennance intensive and can break down. Don't want it to break? No problem, just use the Settings window to make parts unbreakable. It's available after you've researched High-Powered Electrics.
- Added the Solar Flare Experimental Fusion Plant. It's available after you've researched Specialized Electrics. Like the Hot Springs, it too can break down or be set to unbreakable in the Settings window.
- Did a balance pass on the templates. Some weigh less and need less RocketParts than before, others are more expensive. Skilled Engineers give discounts to inflate/reconfigure modules.

ModuleManager Patches
- Relocated the MetallicOre, Metals, and Ironworks MM patches for Extraplanetary Launchpad to a central file called MM_ExtraplanetaryLaunchpads.cfg. This will make it easier to modify Pathfinder to support other production chains than the EL standard MetalOre->Metal->RocketParts.
- Added MM_TACLS.cfg and MM_Snacks.cfg patches to configure Pathfinder to use TACLifeSupport or Snacks, respectively.
- Added MM_CommercialSciLab.txt, an optional MM patch that will enable the Doc Commercial Science Lab's lab functionality on all parts with lab modules. Simply rename MM_CommercialSciLab.txt to MM_CommercialSciLab.cfg if you want to enable this functionality.

Bug Fixes
- Fixed an issue where the module info window would not properly show the template decal.
- Lights should now properly turn themselves off when deflating a part.
- You won't be able to turn on the lights when a module is deflated.
- Resource converter efficiencies are now being calculated properly.
- Fixed an issue where converting storage to a battery was reporting incorrect values.
- If you stack a box of RocketParts onto a Ponderosa and try to inflate it, the Ponderosa will now properly check to make sure it has enough parts.
- Drills will now remember the correct resource that you switched them to.

Recommended Mods
TACLifeSupport: http://forum.kerbalspaceprogram.com/threads/40667-1-0-2-TAC-Life-Support-v0-11-1-20-5Apr
Snacks!: http://forum.kerbalspaceprogram.com/threads/90841-1-0-2-Snacks!-Kerbal-simplified-life-support-v0-3-5
kOS Scriptable Autopilot System: http://forum.kerbalspaceprogram.com/threads/68089-1-0-4-kOS-Scriptable-Autopilot-System-v0-17-3-2015-6-27
Action Groups Extended: http://forum.kerbalspaceprogram.com/threads/74195-1-0-4-%28Jul09-15%29-Action-Groups-Extended-250-Action-Groups-in-flight-editing-Now-kOS-RemoteTech

0.7.0: The Magificent Seven
youtube: Magnificent Seven Theme

NOTE: Please pack up your base before applying this update, there were a lot of changes under the hood.
NOTE: Please be sure to update your OSEWorkshop to the very latest version.

For science! The Gold Digger generates experiment results while taking core samples. Those results are now worth more than just their Science value; you can use them to improve the efficiency of your habitation, science, and industry processors. In other words, creating RocketParts, MaterialKits, ResearchKits, Water, and others will be improved if you have good results when performing your soil analysis, metallurgic analysis, or chemical analysis in the Geology Lab. And with the Geology Lab’s new Biome Analysis, you can either use the research to improve your production abilities within the biome, or transfer the data to the new Doc Commercial Science Lab. It will cost you Science to perform a Biome Analysis, but you stand to gain much more.

New Parts
- Added the Doc Commercial Science Lab. This is a new multipurpose laboratory. With the Doc Commercial Science Lab, developed in partnership with the Rasta Engineering Group (thanks rasta013 for the lab configs!), you get a Mobile Processing Lab equivalent that can do more than just process experiments for Science over time; you can publish the research for Reputation, or sell the research for Funds. You can also reconfigure the lab into the Sunburn pellet lab, and produce FusionPellets and Coolant for your fusion needs. And finally, you can retool it into the Watney Chemistry Lab, named after a fictional character that finds creative survival solutions through chemistry.

- Added the Hacienda Inflatable Multipurpose Factory. The Hacienda meets your industrial needs by providing the Ironworks, where you can smelt Metals from MetallicOre and create RocketParts from Metals. If you have Extraplanetary Launchpads installed, then you'll also get the Fireworks Survey Station, where you can build vessels. Also you smelt Metal from MetalOre, and fabricate RocketParts from Metal instead of using MetallicOre and Metals. Be sure to bring some survey stakes and a mallet. And if you have OSE Workshop installed, then you'll get the Clockworks, which lets you 3D print parts in larger volumes than the Blacksmith, and you can retool the printers to use RocketParts instead of MaterialKits (or vice versa). Additionally, with OSE Workshop installed, the Ironworks lets you produce MaterialKits, and convert between MaterialKits and RocketParts.

Saddle
- Updated config file to reflect the latest KIS.

Storage Templates
- Added templates for Metals and MetallicOre. These will turn into Metal and MetalOre with Extraplanetary Launchpads installed.

Ponderosa & Buckboard
- Now you can stack the Ponderosa and Buckboard on top of each other via convienient stacking nodes.

Ponderosa Templates

- Geology Lab: Added the ability to generate a Biome Analysis. Be sure to carefully read the tooltip.
- The Geology Lab can use core samples to perform a soil analysis, metallurgic analysis, or chemical analysis. A Soil analysis improves production efficiency for life support (coming soon!), a metallurgic analysis improves fabrication processors (Ironworks), and a chemical analysis improves chemical processors (Watney lab, Sunburn lab).
- When unlocking a biome for the first time and generating Science in the process, the Geology Lab will use the stock experiment results dialog to display the fruits of your labor.
- The Geology Lab can uplink to an orbiting T.E.R.R.A.I.N. equipped satellite/station to check on its status and switch to the vessel as desired.

TERRAIN
- Added a "Review Data" button to review the Science collected by the TERRAIN. It will display the stock experiment results dialog.
- Added some feedback fields to the right-click menu.

Gaslight, Ponderosa, Chuckwagon
- Added ability to toggle the lights via Lights action group.

Bug Fixes
- Fixed an issue where you'd see "No need to reconfigure to the same module type" when switching templates.
- Fixed an issue with the Gold Digger's ability to know which tech node activates its ability to drill for resources.
- Fixed an issue preventing the Blacksmith from printing parts. (NOTE: You'll need the latest version of OSEWorkshop as well)

Recommended Mods
Extraplanetary Launchpads: http://forum.kerbalspaceprogram.com/threads/59545-1-0-4-Extraplanetary-Launchpads-v5-2-1

0.1.6: Neon Light

Need a light? The Gaslight Telescopic Lamppost has you covered. Plant it in the ground (if you can run into it and move it, it’s not planted properly), extend the pole, and turn on the lights. It has a small battery built in, but for continual use, be sure to plug it into your base using its built-in KAS ports. It even serves as a short-range omnidirectional antenna- thanks for the suggestion, MeCripp, hope you like the MC-16 communications link. :) Don’t like the color? Are the lights too bright? You can change them in the field through the light’s right-click menu. 

This update supports the OSE Workshop as a new template for the Ponderosa, and adds a new MaterialKits storage template as well. OSEWorkshop lets you 3D print individual parts like hammers, the Mk1 command pod, and even another Ponderosa. If you download OSE Workshop (I recommended the mod), you’ll be able to 3D print parts if you have enough MaterialKits.

In keeping with the mod's spirit of jurry-rigging what you need, you can now re-engineer the Buckboard and Outback into batteries. This is a revision to the battery template added last update. Additionally, you can now reconfigure drills to drill for different resources. You'll see this concept of jurry-rigging in other parts in the future.

Finally, there are some bug fixes, infrastructure changes, and integrated standard KAS ports to help keep your base’s part count down.

NOTE: Please retire your existing TERRAIN scanners (unless you're savy enough to open your save file and replace PhotoSupplies with ResearchKits).

New Parts
- Added the Gaslight Telescopic Lamppost. It comes with a 4-way KAS pipe junction.

Saddle
- Added a pair of integrated KAS pipes.

Outback
- If attached to a command pod that can hold crew, the Outback will have the ability to recharge its EVA Propellant.

Templates
- Added the Blacksmith 3D Print Shop template to the Ponderosa. This will only be available if you have OSE Workshop installed (I recommend the mod).
- Added a new storage template to hold MaterialKits.
- Renamed PhotoSupplies to ResearchKits, and changed the icon.
- Changed the icon for the Ponderosa Habitat.
- The Battery template's EC levels have been nerfed; The Outback holds 154EC, while the Buckboard holds 611EC (comparable to a stack of three Z-200 batteries). Additionally, you'll need RocketParts and an Engineer to convert the Outback/Buckboard into batteries (which of course can be turned off using the Settings window). To convert the Buckboard/Outback into a battery, right-click the part to open the action menu, and press the "Convert to battery" button. You can convert the Buckboard/Outback back into a storage container as well.

Other
- Created a wiki page describing how to add a template to the Ponderosa: https://github.com/Angel-125/Pathfinder/wiki/Anatomy-Of-A-Template

Bug Fixes
- Fixed an issue where the Buckboard would incorrectly show a ToggleAnimation button during EVA.

Recommended Mods
OSE Workshop: http://forum.kerbalspaceprogram.com/threads/108234-1-0-2-OSE-Workshop-MKS-KIS-Addon-%28v0-7-3-2015-06-01%29

0.1.5: Bonanza

Orbiting satellites are great for detecting resources from space, but advanced sensors take a long time to research. With this update, Pathfinder introduces resource scanning tech earlier in the game- assuming it works. Additionally, the Geology Lab can do things that the stock surface scanner can- and more if you staff it right. Finally, the new Outback gives you a handy way to haul small amounts of resources around without lugging the Buckboard.

New Parts
- Added the T.E.R.R.A.I.N. Geo Scanner. If it breaks down, you'll have to repair it. Don't want it to break? You can change it in the settings menu. 
- Added the Outback Extravehicular Support Pack (ESP). It's great for hauling a small amount of resources around, and it's EVA friendly. It can be attached to the exterior of a vessel by pressing the "H" key, and detached using the "G" key. Anybody can use it.

Ponderosa
- Cleaned up the Ponderosa right-click menu and moved a lot of functionality to the Ponderosa Operations window. Access it via the Manage Operations button in the right-click menu.

Pathfinder Geology Lab
- The Geology Lab is now controlled through the Operations window. Simply right-click on the Ponderosa, press the Manage Operations button, and press the Show button to manage the Geology Lab.
- The Geology Lab can now perform a surface analysis of the biome if properly staffed.
- If you have the Impact mod (I recommend it), the Geology Lab can lend its seismometer to the cause if properly staffed.

Templates
- Added the Prime Flux Battery template to the Buckboard and Outback, named after Prime Flux, who suggested the idea. Thanks Prime Flux! :)

ToolTips & Settings
- Added a tooltip to remind players that the requirement to pay for redecorating/inflating the Ponderosa can be turned off.
- The Settings window can now be toggled using the modifier key (which defaults to the Alt key on Windows) plus P instead of being hard coded to the Alt key.

Resources & Storage
- Added the PhotoMaterials resource for taking pictures.
- Adjusted the Buckboard's storage capacity when not being used to store KIS items.
- You can now access the Chuckwagon's inventory from inside the module.

KIS/KAS
- Updated the saddle to account for the latest changes in KIS.
- Thanks to the latest version of KIS, non-engineers can attach the drill to a vessel with the "H" key and grab it with the "G" key.

Recommended Mod
Impact: http://forum.kerbalspaceprogram.com/threads/114087-1-0-Impact!-impact-science-and-contracts-v1-1-0-With-Asteroids-30-6-15

0.1.4: Raw Hide

Developer Notes: When I started working on Multipurpose Colony Modules, I always intended to have the player spend resources to reconfigure the module. Its successor, Pathfinder, finally realizes that vision. Similarly, certain templates require a specific skill to reconfigure the module into its new configuration.

The new requirements add a new challenge to the gameplay without overly complicating the system. But as always, it’s your game, your choice, so you can disable these requirements if you prefer not to play with them. Simply press Alt P to bring up the Pathfinder settings window, and uncheck the box next to “requires resources to reconfigure.” If you uncheck the box, the Ponderosa won’t require resources to inflate and outfit the module either. Similarly, you can disable the skill requirement.

IMPORTANT NOTE: The directory structure has changed. Please delete the WildBlueIndustries folder before installing this update.
NOTE: Please pack up your Ponderosas before applying this patch.

- The Ponderosa now requires RocketParts to inflate the module and to reconfigure it. Be sure to have an ample supply of RocketParts on your vessel. If you prefer to not use this feature, simply press Alt P to bring up the Pathfinder Settings window to disable it.
- Added the Conestoga Multipurpose Logistics Module (MLM). The Conestoga holds a lot more stuff than the Buckboard, but it’s not very hand-portable.
- Added the Mineshaft Portable Crew Tube (PCT). Mount one on each Ponderosa that you want to connect to, and then link them together, just like the KAS pipe.
- Added the Ponderosa Habitat template to the Ponderosa. It will be helpful in the future, but right now it's decorative.

0.1.3: Dead Or Alive

NOTE: Please pack up your base before applying this patch.
- Adjusted the Buckboard storage capacity to something more reasonable and carryable.
- Removed attach_node from the Saddle; I think that's causing the Kraken to take notice.
- You can now access the Ponderosa's inventory from inside the module as well as during EVA.

0.1.1: Blaze Of Glory

Want to camp out? Then take along the Ponderosa Inflatable Crew Module (ICM)! First bolt a Saddle into the ground, then saddle up the Ponderosa. The ICM comes outfitted with the Pathfinder Geology Lab, but other configurations are possible. And if you need supplies, don't forget to bring along the Buckboard shipping container.

- Added the Saddle Mini-Slab. Attach this to the ground.
- Added the Ponderosa Inflatable Crew Module with Pathfinder Geology Lab.
- Added the MC-1000 Buckboard, a container able to hold a variety of different resources.

0.1.0: Save My Soul

NOTE: This is a technology demo As such, parts and functionality are subject to change.

Do you have what it takes to be a prospector? Then step right up and try your luck with the Gold Digger Portable Mini-Drill! Take core samples for science! Analyze the results! Don't eat the samples.

- Introducing the Gold Digger Portable Mini-Drill.

---ACKNOWLEDGEMENTS

Module Manager by saribian
Community Resource Pack by RoverDude, Nertea, and the KSP community
Portions of this codebase include source by Snjo and Swamp-IG, used under CC BY-NC SA 4.0 license
Icons by icons8: https://icons8.com/license/
Eve: Order Zero graphic courtesy of Kuzztler and used with permission.

---LICENSE---

Source code copyrighgt 2015, by Michael Billard (Angel-125)
License: CC BY-NC-SA 4.0
License URL: https://creativecommons.org/licenses/by-nc-sa/4.0/
Wild Blue Industries is trademarked by Michael Billard and may be used for non-commercial purposes. All other rights reserved.
Note that Wild Blue Industries is a ficticious entity 
created for entertainment purposes. It is in no way meant to represent a real entity.
Any similarity to a real entity is purely coincidental.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.