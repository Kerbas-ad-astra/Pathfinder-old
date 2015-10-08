![](https://github.com/Angel-125/Pathfinder/wiki/Chuckwagon.jpg)  
**Mass:** 0.025t (empty, loaded mass varies)  
**Cost:** 500  
**Research:** Advanced Construction  
**Research Cost:** 4,000  
**Max Temp:** 2,000 K  
**Max Storage Volume:** 31,320 L 

The Chuckwagon Inflatable Multipurpose Warehouse (IMW) holds a large amount of different resources. It requires the [Kerbal Inventory System (KIS)](http://forum.kerbalspaceprogram.com/threads/113111-1-0-4-Kerbal-Inventory-System-%28KIS%29-1-2-2) in order to carry it on a kerbal's back. You can radially attach the Chuckwagon to your vessel. It takes an Engineer to assemble the IMW.
#Usage  
To configure the Chuckwagon to store a different resource, simply right-click on the part to bring up its context menu, and press the Next or Prev buttons to change the desired resource. In the VAB/SPH and out in the field, the Chuckwagon will switch to the new resource. If you have resources stored in the container, you will be asked to click the button again to confirm switching. At the top of the context menu is a status field showing you what the IMW currently holds. Finally, if you want to show or hide the decals, simply press the Toggle Decals button.
![](https://github.com/Angel-125/Pathfinder/wiki/ChuckwagonUsage.jpg)  

The Chuckwagon can be converted to a greenhouse by a Scientist. It costs 600 RocketParts to do so, unless you disable the need for RocketParts in the [[Pathfinder Settings|Pathfinder-Settings]] window. Simply right-click on the IMW to open the context menu, and press the Convert to greenhouse button. You can convert it back to storage by pressing the Convert to storage button.
# Designer's Notes  
The original Chuckwagon comes from Multipurpose Colony Modules, and it was just called the Inflatable Multipurpose Warehouse. I also set up the original model so that I could turn it into a greenhouse. When I brought it over to Pathfinder, at first all I did was rework its connector tube to Pathfinder's 1m diameter standard, and turn the top of the dome into windows. Then I started adding crew ports to all the inflatable modules, and the Chuckwagon gained its own set. Finally, I realized my dream of turning the Chuckwagon into a greenhouse, and gave it a pair of inflatable airlock ports. As with other Pathfinder storage parts, the IMW is named after an Old West wagon.

In designing the greenhouse, I researched what you’d need in order to grow potatoes. One article I found is this one, claiming that you can grow 100lbs of potatoes in 4 square feet: http://tipnut.com/grow-potatoes/ 4 square feet works out to 45.36kg/.37m2 = 122.6kg/m2 = 0.1226tonnes/m2. How does that translate into food consumption? We know how many kilograms we’ll produce with 0.37m2 of floor space, now we need to translate that into the volume and density of Food that TAC and CRP uses.  
TAC’s calculations say that a kerbal needs 0.365 units of Food per day. The density of Food is 0.00028102905982906tonnes/unit, where 1u = 1L. According to aqua-calc.com, 1L of potatoes masses 0.89kg (0.00089tonnes). So:  
0.1226 tonnes/m2 / 0.00089 tonnes/u = 137.75u/m2.  
Assume that a converted Chuckwagon has a usable floor space that’s 4 meters in diameter. It’s actually less than that since the module itself is about 4 meters in diameter. With 4m of space, we have an area of: 12.56m2. 
How many units of Food can we produce with 12.56m2 of floor space? 12.56m2 * 137.75u/m2 = 1730.18u of potatoes. That’s a lot of Food! By TAC standards, Kerbals eat about a quarter of what a human does, so let’s scale this back accordingly. Let’s assume a quarter yield because kerbal potato plants are smaller:  
1730.75 / 4 = 432.54. How many kerbal days is that? 432.54 / 0.365 = 1185 kerbal-days. That’s still a lot! We’ll keep scaling this back. Figure that you can produce 1 kerbal-year worth of food regardless of system. So for TAC, you produce 155.5 units of food, and for Snacks, you produce 426 snacks. That works, but really, it makes the greenhouse still overpowered. So let’s say that it produces 180 kerbal-days worth of Food/Snacks. That’s enough to feed a crew of two for 90 days, or a crew of 4 for 45 days. I think that’s reasonable.  
Ok, what about water? According to bettervegetablegardening.com, potatoes need 2.5 to 5cm of water per week, and you get best results with 20-30cm. Water use is 4 to 7 kg per cubic meter of soil per week to grow potatoes. 90 days uses up 5kg of water (5 units). 5/90  = 0.0556u/day = 2.57 * 10^-6 = 0.00000257.