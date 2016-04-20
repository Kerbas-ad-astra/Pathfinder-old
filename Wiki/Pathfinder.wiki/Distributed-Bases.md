In Pathfinder, one option for preventing the Kraken from eating your large sprawling bases is to make smaller ones. But that presents a new problem: how do you share resources between your clusters of bases? With Pathfinder, that's now pretty easy. The [[Ponderosa Inflatable Module|Ponderosa]], [[Casa Radial Inflatable Module|Casa]], [[Doc Science Lab|Doc-Commercial-Science-Lab]], [[Hacienda Inflatable Factory Module|Hacienda]], [[Chuckwagon Inflatable Multipurpose Warehouse|Chuckwagon-Inflatable-Multipurpose-Warehouse]], and [[Conestoga Multipurpose Logistics Module|Conestoga-Multipurpose-Logistics-Module]] all have the option to share their resources via Pathfinder's resource distribution system. The system is opt-in, meaning that you must decide which modules in your base will share their resources. While not depicted in game, you can assume that there are pipes that run between your base clusters to share resources.  
  
Each module capable of sharing its resources is known simply as a distributor. In Pathfinder, you can enable and disable individual distributors independently of each other, and for each distributor, you can exclude a resource from being shared simply by locking it. And for convenience, you can enable and disable all the distributors in your currently focused base through the [[Pathfinder Settings|Pathfinder-Settings]] window.  
  
Pathfinder periodically distributes your resources, and you have the ability to control how often the distribution takes place. You can also elect to distribute your resources immediately. Both functions are performed through the Pathfinder Settings window; see below for details.  

Here's how resource distribution works:  
  
Every few seconds, Pathfinder will check each vessel within physics range for any active distributors. If it finds any, it checks the distributor's list of resources that can be shared. Of those resources, it takes the total sum of the resource and divides it up between the active distributors containing that resource. For example, if you have a base that produces Water and it has 3,000 units, and you have three bases that can hold 1,000 units but only have 200 units at present, then Pathfinder will see that you have a total of 3,600 units of water. By the time it is done, the distribution system will make sure that each of the 1,000-unit containers has 600 units of water and that the 3,000-unit container has 1,800 units.  
    
Priority is given to parts that have converters that require resources. For instance, the [[Prairie Greenhouse|Prairie-Greenhouse]] requires Dirt. In this case, Pathfinder will fill any distributor's container with the required resource until it is full before taking the remainder and parceling it out to the other distributors.  
# Usage  
Listed below are directions on how to use the resource distribution system.  
## Opt-In (Entire Part)
1.	Right-click on the part.
2.	Press the Distributor button until the button is depressed and the UI says “ON”
3.	For any resource you wish to distribute, make sure that the resource isn’t locked.
Note: If a resource is required by a converter, then it will not contribute its resources to the grand total, but it will take from the grand total until completely full.  
  
## Opt-In (Entire Active Vessel)
1.	Open the [[Pathfinder Settings|Pathfinder-Settings]] window.
2.	Navigate to the Distribution tab.
3.	Press the Active Vessel: Opt-In button.  
  
## Opt-Out (Individual resource)
1.	Right-click on the part and make sure that the Distributor is on.
2.	Next to the resource that you want to opt-out, click the lock button.
Note: If a resource is required by a converter, then it will automatically opt-out when it gets full.  
  
## Opt-Out (Entire Part)
1.	Right-click on the part.
2.	Press the Distributor button until the button is not depressed and the UI says “OFF”  
  
## Opt-Out (Entire Active Vessel)
1.	Open the [[Pathfinder Settings|Pathfinder-Settings]] window.
2.	Navigate to the Distribution tab.
3.	Press the Active Vessel: Opt-Out button.  
  
## Manually Distribute Resources
1.	Open the [[Pathfinder Settings|Pathfinder-Settings]] window.
2.	Navigate to the Distribution tab.
3.	Press the Distribute Resources button.  
  
## Configure Distribution Timer
1.	Open the [[Pathfinder Settings|Pathfinder-Settings]] window.
2.	Navigate to the Distribution tab.
3.	In the Distribution Timer field, enter the number of seconds (game-time) to wait until redistributing your resources.

# Designer's Notes  
Designing the resource distribution system was a lot of fun. I went through several iterations of the architecture and a number of spreadsheet calculations to come up with a solution that was fast and minimized the system memory needed. Figuring out how to calculate how much of a resource each distributor was also a challenge. My first couple of attempts just flat out didn't work. Then my girlfriend pointed out that I should just fill the distributors with required resources (some converters have required resources in order to function, such as the Greenhouse's Dirt requirement) and then distribute the remaining amount to the other distributors. That simplified things. Then I found a better way to compute each distributor's portion size, and that saved me an extra computing step. All in all, I'm quite happy with the end result. :)