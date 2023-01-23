# asteroids-scriptable-objects-Assignment
Submission for the Futuregames 2023 Tool development Course


Reflections and Learning 

From doing this assignment, the clearest benefits of scriptable objects was consistency of reference. 
Rather than having many copies of data or structs saved in prefabs, many objects can share what is effectively 
an object reference to a single object in memory.

This can make it substantially easier for designers to ensure that updates to the base data 
assumptions of a project cascade appropriately through every reference to that data. For complicated structs or data structures, a single reference
to an object can substantially reduce the effective memory print of any object that needs the use of that data structure.

Where I think care must be taken is in how these values are accessed. I can envision a scenario where many many simple objects
using a method property call to a single scriptable object holding some data could cause issues, and potentialy finding a way for 
that data to be moved internally to a class and updated on changes might produce speed improvements. It would be a highly specialized 
area however, and often the ease of design control would probably be worth this potential trade off.
 
With regards to the Unity Atoms framework, I am of two minds. On the one hand, the event as object design mythos is an extremely 
valuable tool for designers to do iteration and gameplay flow. That it allows changes to occur to game logic without requiring 
debugging of existing code upon a change is monumental. 

I am less enthusiastic about some parts of the Atom package that seem to have expanded in ways where I do not quite understand what the gain
is of using them. In particular, the variable instancers class are not intuitive to me. It's possible with more experience I will come to
see what problems these are designed to solve but for now I see more immediate value in the Atomic Scriptable object methodology in its
Event and Variable packages.

The value of UI Toolkit was less apparent at first. It is quite different to the coding for other aspects of unity, but the professional implications
of standardizing game UI with existing web presentation technoligies are very obvious. 

It's also a belated embrace of Separation of Concerns for UI, which has always existed in a slightly over componented based form in the Unity engine.

My biggest problem during this assignment was trying to rectify ability of UI toolkit to autobind to serialized objects with how 
enums are represented in the Atomic scriptable object methodlogy. The pair work substantially less well with enums than other variable types, which
I think is due to the class like nature of an enum; as a holder of state, it cannot be made generic in the same way as a float without multiple layers of
abstraction being used, reducing it's designer friendliness.
