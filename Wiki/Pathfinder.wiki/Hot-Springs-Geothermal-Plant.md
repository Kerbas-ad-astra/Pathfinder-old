![](https://github.com/Angel-125/Pathfinder/wiki/HotSprings.jpg)  
**Mass:** 5t  
**Cost:** 2,000 RocketParts  
**Research:** Large Electrics

The Hot Springs Geothermal Plant uses geothermal energy from the planet or moon to generate ElectricCharge. It is maintenance heavy though; the Hot Springs uses 0.001 RocketParts per second (21.6 RocketParts per day). The RocketParts represent the drilling equipment needed to dig into the ground as well as the equipment that gets used up in the process. On the plus side, the Hot Springs generates plenty of power for your base.

Below is the ops window for the Hot Springs:  
![](https://github.com/Angel-125/Pathfinder/wiki/HotSpringsOpsView1.jpg)  

There are no converters available for the geothermal plant, but it does have a custom view:  
![](https://github.com/Angel-125/Pathfinder/wiki/HotSpringsOpsView2.jpg)  
**Geothermal Status (1):** This field shows the current status of the geothermal plant.  
**Geothermal Efficiency (2):** This field shows you how efficient the power plant is. The rating is affected by the planet or moon that the base is on as well as the current biome's efficiency bonus for Metallurgy.  
**Time Until Maintenance (3):** This field shows you how long you have until the next maintenance check. Once time runs out, Pathfinder makes a check to see if the plant can continue to operate. A good result means no change, while a bad result increases the probability that the plant will fail on the next maintenance check. A catastrophic result means that the plant stops operating and needs repairs.  
***  
You'll need 500 RocketParts to effect repairs. Skilled Engineers can reduce that amount. Remember, you can adjust the need for RocketParts in the [[Pathfinder Settings|Pathfinder-Settings]] window, and eliminate the possibility of breakage altogether.  
***  
**Failure Probability (4):** This field lists the probability that the Hot Springs will fail on its next maintenance check.  
**Generator Control (5):** Press this button to start or stop the power generation.
# Designer's Notes  
You can't have a mod based on geology research without having a geothermal energy plant. I wanted something that would work practically everywhere, but some on the forums suggested to limit the plant to a few planets and moons. Then I found out that [the Moon isn't geologically dead!](http://www.space.com/14632-moon-dead-geologic-activity-monitored.html) That's all I needed to know to apply my "plausible deniability" approach to physics in order to let the Hot Springs work everywhere. All I needed to do is vary the output depending upon which planet or moon the base was on.