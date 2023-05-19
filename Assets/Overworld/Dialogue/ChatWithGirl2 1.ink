-> welcome
== welcome ==
Hey Player # speaker: Girl # side: RIGHT
Hey Girl # speaker: Player # side: LEFT
So, waddaya want? # speaker: Girl # side: RIGHT
-> mainSubject
=== mainSubject ===
{ IsQuestStarted(0) == false:
+ [Any quest for me?] 
    -> getQuest0
- else:  
+ [About that quest...] 
    -> aboutQuest0
}
 + [Just talk.] -> smallTalk
 + [Bye]
     Nevermind, bye. # speaker: Player # side: LEFT
     Seeya. # speaker: Girl # side: RIGHT
    -> END


=== getQuest0 ===
Any quest for me? # speaker: Player # side: LEFT
Ok, i need you to go and win the fight. # speaker: Girl # side: RIGHT
There is a meatblock nearby. Just enter it and win the fight. Come back to me when you are done. # speaker: Girl # side: RIGHT
~StartQuest(0)
Anything else? # speaker: Girl # side: RIGHT
->mainSubject

=== aboutQuest0==
About that quest... # speaker: Player # side: LEFT
Yea? youve won? # speaker: Girl # side: RIGHT
{ IsQuestTaskFinished(0,1) == false:
Not yet... # speaker: Player # side: LEFT
So why are you bothering me?
Anything else? # speaker: Girl # side: RIGHT
    - else:  
Sure, ive won! # speaker: Player # side: LEFT
Whose the big boy!? Congratz! # speaker: Girl # side: RIGHT
~ProgressQuestTask(0, 3, 1)
Anything else? # speaker: Girl # side: RIGHT
}
->mainSubject

=== smallTalk ===
Lets talk. # speaker: Player # side: LEFT
Well, i can tell you about this place, myself or show you feet.# speaker: Girl # side: RIGHT

+ [Place]
    Tell me more about this place. # speaker: Player # side: LEFT
    This is a debug area, nothing less blah, blah blah. # speaker: Girl # side: RIGHT
    Here is second dialogue, and, um, lets change subject. # speaker: Girl # side: RIGHT
    ->smallTalk
+ [You]
    Id listen about you # speaker: Player # side: LEFT
    Well im girl, and i chill here near meatblock and other NPC girl. # speaker: Girl # side: RIGHT
    I like also bananas, apples and cats. # speaker: Girl # side: RIGHT
    Anything else? # speaker: Girl # side: RIGHT
    ->smallTalk
+ [Feet]
    hjehe feet hehe # speaker: Player # side: LEFT
    ->smallTalk
+ [Nevermind]
    Nevermind, lets talk about sth else # speaker: Player # side: LEFT
    ->mainSubject
+ [Bye]
    -> END
    
    
EXTERNAL IsQuestStarted(questID)
EXTERNAL StartQuest(questID)
EXTERNAL ProgressQuestTask(questID, taskID, progress)
EXTERNAL IsQuestTaskFinished(questID, taskID)

=== function IsQuestStarted(questID) ===
~ return false
=== function StartQuest(questID) ===
~ return false
=== function ProgressQuestTask(questID, taskID, progress) ===
~ return false
=== function IsQuestTaskFinished(questID, taskID) ===
~ return false

