![](https://github.com/Angel-125/Pathfinder/wiki/HotSprings.jpg)  
**Mass:** 5t  
**Cost:** 2,000 Equipment  
**Research:** Large Electrics

The Hot Springs Geothermal Plant uses geothermal energy from the planet or moon to generate ElectricCharge. In game this resource is known as GeoEnergy. You'll need to land on a geologically active world in order to make use of this generator, but not all planets and moons are geologically active. The table below shows the likelihood that a world will be geologically active and thus have GeoEnergy in your current game:  

| World            | Percent Chance      | Notes |
| :-------------: |:-------------:|:-------------|
| Sun | 0% |   |
| Moho | 70% |   |
| Eve | 100% |   |
| Gilly | 0% |   |
| Kerbin | 100% |   |
| Mun | 85% | [Apparently Earth's Moon is geologically active!](http://www.space.com/14632-moon-dead-geologic-activity-monitored.html)  |
| Minmus | 0% | [Wiki suggests that it is a captured comet](http://wiki.kerbalspaceprogram.com/wiki/Minmus)  |
| Duna | 70% | [Mars could be active](http://astrogeo.oxfordjournals.org/content/44/4/4.16.full)  |
| Ike | 70% |   |
| Jool | 0% |   |
| Laythe | 100% |   |
| Val | 70% |   |
| Tylo | 60% |   |
| Bop | 0% | Captured asteroid, too small to be active  |
| Pol | 0% | Captured asteroid, too small to be active  |
| Dres | 0% | Asteroid, too small to be active  |
| Eeloo | 100% | [Pluto is active!](http://io9.com/breaking-geologic-activity-has-been-detected-on-the-su-1718055390)  |  
If a world is not on the above list then it has a base 70% chance of having GeoEnergy.  

In addition to GeoEnergy, the Hot Springs needs 5,000 liters of Water to work its steam turbines. Built-in condensers will reclaim the steam; there are slow losses over time, however. You can either drill for water on site or ship in your own supply. If you don't have enough Water stored in the Hot Springs, then the generator will not function.

Below is the ops window for the Hot Springs:  
![](https://github.com/Angel-125/Pathfinder/wiki/HotSpringsOpsView1.jpg)  

There are no converters available for the geothermal plant, but it does have a custom view:  
![](https://github.com/Angel-125/Pathfinder/wiki/HotSpringsOpsView2.jpg)  
**Status (1):** This field shows the current status of the geothermal plant.  
**Efficiency (2):** This field shows you how efficient the power plant is. The rating is affected by the current biome's efficiency bonus for Metallurgy.  
**Time Until Maintenance (3):** This field shows you how long you have until the next maintenance check. Once time runs out, Pathfinder makes a check to see if the plant can continue to operate. A good result means no change, while a bad result increases the probability that the plant will fail on the next maintenance check. A catastrophic result means that the plant stops operating and needs repairs.  
***  
You'll need 500 RocketParts to effect repairs. Skilled Engineers can reduce that amount. Remember, you can adjust the need for RocketParts in the [[Pathfinder Settings|Pathfinder-Settings]] window, and eliminate the possibility of breakage altogether.  
***  
**Failure Probability (4):** This field lists the probability that the Hot Springs will fail on its next maintenance check. A good result will not change the failure probability, while a great result will actually reduce the failure probability. By contrast, a failed check will reduce the failure probability, and a catastrophic result will break the plant.  
**Geothermal Power (5):** Press this button to start or stop the power generation.  
**Geothermal Tap (6):** Press this button to start or stop the geothermal tap. You will need a supply of GeoEnergy along with Water in order to power you generator.  
# Designer's Notes  
You can't have a mod based on geology research without having a geothermal energy plant. I wanted something that would work practically everywhere, but some on the forums suggested to limit the plant to a few planets and moons. Then I found out that [the Moon isn't geologically dead!](http://www.space.com/14632-moon-dead-geologic-activity-monitored.html) That's all I needed to know to apply my "plausible deniability" approach to physics in order to let the Hot Springs work on many planets in the Kerbol system.

The latest version of the Hot Springs makes it work more realistically without overly complicating the system or introducing too many resources. I think it's a nice balance between really realistic and good game play.