// Insanity state
VAR insanity = 0

// Insanity tracker
\**INSANITY METER = {insanity}**

// Items
VAR dictionary = false
VAR fontpack = false
VAR photocopies = false
VAR thesaurus = false

// Character status
VAR constabletaken = false
VAR mayortaken = false

// Debug mode
VAR DEBUG = false
{DEBUG:
        \-----IN DEBUG MODE-----
        *   [First Quarter...] -> meetingone
        *   [Waning Crescent...] -> meetingtwo
        *   [Full Moon...] -> meetingthree
        *   [Endings...] -> endinghandler
    - else:
        -> meetingone
}

/* Function for changing insanity */
=== function change(ref x, y)
    ~x = x + y

/*                                      */
// GAME STARTS HERE

=== meetingone
// Intro
    -   The following minutes were taken at the regular monthly meeting of the Mayor and the City Board on 1 September 2018 at the Town Hall in
        *   Launceston[], Tasmania.
        Mayor Van Zetten called the meeting to order at 7:01pm - First Quarter.
    - (mayor)
        *   [Transcribe the Mayor's announcement]MAYOR'S ANNOUNCEMENTS:
            Mayor Van Zetten announced the passing of long time Launceston resident and beloved minutetaker, Bob Olmstead.
            His son, James Olmstead took his place as minutetaker for the City of Launceston.
    
            Mayor Van Zetten opened the meeting by reminding residents that the annual renewal of the Town Pact would be lapsing next month.
            The following meeting, taking place on 17 September, the Waning Crescent of the moon cycle, would be the date the conditions of the New Pact are decided upon.
    
    -   (rollcallone)
        *   [Take attendance]ROLL CALL:
            Constable Jim Williamson, Constable with the City of Launceston Police Department, Ms. Alicia Marsh, and Mr. Barnabas Marsh (no relation), longtime resident of the Midlands, were present.
            Mr. Marsh was unable to address his attendance verbally. Attendance noted.
            **  [Question Mr. Marsh] Ms. Marsh reminded Minutetaker Olmstead that Mr. Marsh was not accustomd to speaking in English, and that he preferred just listening.
                ~ change(insanity, 1)
            **  [Move on]
    
    -   (agendaone)
        The {first|second|third|fourth|-> endagendaone} subject of the agenda was addressed by
        *   [The Mayor] Mayor Van Zetten, who noted the ongoing works in the city's sewerage system. An attendee asked about the liquid that had been appearing from home faucets, which was said to cause sweats and mild hallucinations after drinking it.
            **  [Press him] The Mayor explained that the presence that had existed beneath the city for thousands (?) of years had affected the quality of the city's water supply beyond an acceptible quality, and thanked the gentleman for his patience and understanding.
                ~ change(insanity, 1)
                *** [Press him] Ms. Marsh interrupted and reminded the meeting that there were other matters on this meeting's agenda.
                ~ change(insanity, 1)
                *** [Move on]  

            **  [Leave it] The Mayor thanked the gentleman for his patience and understanding.
    
        *   Jim Williamson[]. Constable Williamson reported that he received complaints from homeowners that they had seen "unsettling" rubbish appearing in their backyard, as well as
            **  [beer bottles]"beer bottles" scattered around their property. No further delinquent activity was reported.
                ~ change(insanity, -1)
                
            **  [the skin of small animals such as rabbits]{insanity > 0: "the skin of small animals such as rabbits", stained by dried blood and splayed out in runic patterns across their front lawns and emitting a terrible stench.}{insanity < 0: "the skin of small animals such as rabbits" being laid in patterns across their front lawns.}
                ~ change(insanity, 1)
                
                Upon further inspection he noted their gardens were dug up and
                *** ["graffiti"] was found on the outside walls of their homes.
                    ~ change(insanity, -1)
                    *** [Press him] The Constable noted that the graffiti was in orange spraypaint.
                    ~ change(insanity, -1)
                    *** [Move on]
                    
                *** ["strange markings"] were left against their garage doors{insanity > 0: arranged in words of a non-linear alphabet.}{insanity < 0:.}
                    The Constable requested that if the old rituals are to be continued they be taken elsewhere. Constable Williamson added he "preferred the previous Old One(s)" because "they kept it clean, at least."
                        ~ change(insanity, 1)
                    ****    [Press him]Mayor Van Zetten reminded Minutetaker Olmstead that it was not his place to comment on agenda matters. Constable Williamson said answered anyway, and said the markings were identified as an old language only communicated by visual, non-linear m,eans.
                        At this time, Mrs. Marsh pointed to Mr. Marsh in his chair and commented: "They are not pleased with you."
                        ~ constabletaken = true
                    ****    [Move on]
                       
        *   Alicia Marsh[]. Ms. Marsh addressed the Mayor directly on the minute-keeping. She reminded those in attendance that notes were only to be taken by the assigned Minutekeeper.
            **  [Written notes?] No written notes of any kind were to be taken. Ms. Marsh reminded them this would affect the written messages needed for the New Pact to be carried out.
                ~ change(insanity, 1)
                *** [Press her] Constable Williamson suggested this matter be raised next meeting. All in favour.
                ~ change(insanity, 1)
                *** [Move on]
        
            **  [Verbal notes?]Constable Williamson asked if verbal notes were allowed to be recorded. Mayor Van Zetten reminded him this was permitted under the conditions of the previous Pact.
                ~ change(insanity, 1)
                *** [Press him] Ms. Marsh said this was just a matter of preference for the Old Ones.
                ~ change(insanity, 1)
                *** [Move on]

        *   [Barnabas Marsh]
            Mayor Van Zetten asked Mr. Marsh if he had any motions to raise. Mr. Marsh did not comment - this was expected at Town Hall meetings.
                
        *   [No one]
            No further agenda items were raised.
        ->  endagendaone
                
    -   ->  agendaone

=== endagendaone
    -   At the Meeting Close Mayor, Van Zetten provided Minutetaker Olmstead with an 'English-to-Old Language' dictionary to assist in recording the upcoming Pact Renewal negotiations.
        ~ dictionary = true
        The meeting was adjourned at 7:31pm.
        
        *   Meeting adjourned.
    -
    #NextMeeting=meetingtwo
    -> DONE
        
=== meetingtwo
    -   The following minutes were taken at the regular monthly meeting of the Mayor and the City Board on 13 September 2018 at the Town Hall in Launceston, Tasmania.
        Mayor Van Zetten called the meeting to order at 7:16pm - Waning Crescent.
        He apologised for his lateness{insanity < 0:.}{insanity > 0:, as an Old One that had taken up residence in his backyard needed to be fed.}
        
        *   [Mayor's announcements]

    MAYOR’S ANNOUNCEMENTS:
    The Mayor repeated the conditions of the New Pact agreed upon in the previous meeting on 1 September.
    
    -   (rollcalltwo)
        *   [Take attendance]ROLL CALL:
            Ms. Alicia Marsh{!constabletaken: Constable James Williamson,} and Mr. Barnabas Marsh were present. {constabletaken: Constable James Williamson sent his apologies; he was unable to make the meeting on account of the moon in its Waning Crescent phase.}

    -   (agendatwo)
        The {first|second|third|fourth|-> endagendatwo} the agenda was addressed by
        *   [The Mayor] Mayor Van Zetten reminded the room that they would need to recite the terms of the New Pact in a language the Old Ones could understand. Mayor Van Zetten referred to the dictionary he provided Minutetaker Olmstead last meeting, and stated it was rare to come by and he would be handing out photocopies of relevant runic patterns at the end of the meeting.
        
        *   [Barnabas Marsh] Mr. Marsh was awake during this meeting, much to the surprise of the room.
            Ms. Alicia Marsh acted as translator between the room and Mr. Marsh. (These Minutes record the translation as it was relayed by Ms. Marsh, however the original transcript in Old Language was not recorded).
            Barnabas stated he represented Pht'thya-l'y (sp?) as Visiting Consul, who sends their apologies for not attending the meeting for the last
            **  two meetings[]. He apologised again for his absence.
                ~ change(insanity, -1)
                
            **  ten thousand years[]. He reminded the room the Old Ones found English difficult as it was "too fast" and therefore indistinguishable. He stressed the importance of learning the language of the Old Ones in order for the New Pact to be renewed.
                ~ change(insanity, 1)
                
        *   [Alicia Marsh] When prompted by the Mayor, Mr. Marsh responded on her behalf however acting through Ms. Marsh as translator. It seemed as if when Ms. Marsh was translating the Old Language for Mr. Marsh, she was unable to speak for herself. Ms. Marsh's partner, who was in the audience, stood up and replied that "this was something she always did, no big deal."
            ** [Press her] Minutetaker Olmstead asked Ms. Marsh to clarify.
                The Mayor asked Ms. Marsh on the complaint that was made on 1 September, 1887 (check date against records?). The issue pertained to the unavailability of the quality of meat available in previous Pacts made. At the time, Ms. Marsh noted that
                ***  the Pact required fresh meat[] for its success.
                    Ms. Marsh did not comment on the matter. Motion to ensure fresh livestock available for Pact not passed, postponed until future meetings.
                    ~ change(insanity, 1)
                
                *** they would be changing suppliers[] for their meat supply.
                    Ms. Marsh did not comment further on the matter.
                    ~ change(insanity, -2)

            ** [Leave it]
        
        +   [Minutetaker Olmstead] Minutetaker Olmstead gave an update on the Old One's font package. Olmstead said he tried to install the font package however it was unrecognised by his laptop.  
            Mayor Van Zetten suggested Minutetaker Olmstead consult the Council's IT Department to raise an urgent ticket before next meeting. Ms. Marsh said it was important the Minutetaker was able to type in the Old Language for the New Pact to be successful.
            ~fontpack = true
            
        *   [No one]
        Mayor Van Zetten handed out photocopies of the runes to those in attendance. No further agenda items were raised.
            ~ photocopies = true
        ->  endagendatwo
                
    -   ->  agendatwo
    
=== endagendatwo
        *   Meeting adjourned.
        -
    #NextMeeting=meetingthree
    -> DONE

// Full Moon phase
=== meetingthree
    -   The following minutes were taken at the regular monthly meeting of the Mayor, the City Board, and the Emmissaries of Dagon on 23 September 2018 at the Town Hall in Launceston.
        Mayor Van Zetten called the meeting to order at
        *   7.01pm[] - Full Moon.
        
        *   11111.1111111pm[] - Full Moon.
        
    -   -> startpact
    
=== startpact
        *   [Mayor's announcements]MAYOR'S ANNOUNCEMENTS:
            The Mayor announced that the New Pact would commence at today's meeting.
            The opening address of the New Pact was spoken by Constable Williamson, while the next phases were written down by the Minutetaker.

        *   [The Old One's announcements]THE OLD ONE'S ANNOUNCEMENTS:
            The Old One attended the meeting as a voice present in the room with no clear source, and reminded the meeting it was tired and it was time for the New Pact to claim its payment once more.
            The opening address of the New Pact was spoken by Constable Williamson, while the next phases were written down by the Minutetaker.

    - (summonone)

        *   \**REDACTED**
            The phrase was redacted by the Minutetaker of their own accord and not written down.
            ~ change(insanity, 1)

        *   Ph'nglui mglw'nafh Pht'thya-l'y North Midlands wgah'nagl fhtagn[].
            ~ change(insanity, 1)
            // "In his house at North Midlands dead Pht'thya-l'y lies dreaming"

        *   {dictionary && fontpack} In his house at North Midlands dead Pht'thya-l'y lies in eternal nightmare.
            ~ change(insanity, -1)
            
    - (summontwo)

        *   \(unintelligible)
            The phrase was deemed unintelligible by the Minutetaker and therefore not written down.
            The Mayor uttered a deafening screech that was beyond any frequency imaginable to the human brain, and proceeded to roll his eyes into the back of his head as his arms writhed above his head.
            Mr. Marsh urged the Minutetaker to continue with the ritual before the Mayor traversed into a different plane.
            ~ change(insanity, 1)

        *   {dictionary && fontpack} Pht'thya-l'y sleeps and waits, we rescind our prayers and silence it eternally.            
            ~ change(insanity, -1)

            // DEATH "[It] sleeps/waits and then acts, we send [our] prayers [to thee], answer [us]"
        *   Mglw'nafh fhthagn-ngah cf'ayak 'vulgtmm vugtlag'n[].

    - (summonthree)

        *   {dictionary && fontpack} The Old Ones are forgotten by the people of Launceston. Pht'thya-l'y will sink to sleep eternal. Pht'thya-l'y is forgotten.
            ~ change(insanity, -1)

        *   \(unintelligible)
            The phrase was deemed unintelligible by the Minutetaker and therefore not written down.
            Mr. Marsh leapt forth and seized the Minutetaker's laptoariaierklT$O444444444444t
            al4llllwtl44tttttttttttttttttttttttttttttttttt
            W4TLLT4LTL4W
            OOK0PK090O9KP9
            inutetaker seized control of the laptop once more and
            ~ change(insanity, 1)

            // DEATH "Gone but not forgotten, Pht'thya-l'y sleeps/waits at Launceston, [promising] death to one and all."
        *   Ph'nglui mglw'nafh Pht'thya-l'y Launceston n'gha-ghaa naf'lthagn[].

    -   -> endinghandler
    
=== endinghandler
{   
    -   insanity > 0 && constabletaken && mayortaken:
       #NextMeeting=worstending
        -> DONE

    -   insanity > 0 && constabletaken || mayortaken:
        //badending
        #NextMeeting=worstending
        -> DONE

    -   insanity < 0 && !constabletaken || !mayortaken:
        #NextMeeting=goodending
        -> DONE
        
    -   insanity < 0 && !constabletaken && !mayortaken:
        #NextMeeting=bestending
        -> DONE
    
    -   else:
        #NextMeeting=worstending
        -> DONE
}

/* Worst Ending */
=== worstending
    -   Police log, 29 September 2018 - Full Moon.
    -   On September 29, 2018, I, Officer Allen and Officer Waite responded on an emergency 000 call from Launceston. We had been unable to raise Constable Williamson or any of our colleagues at the Launceston Police Department for several hours.
    
        Upon arrival we noted that there was no significant emergency in Launceston. The Mayor of the town, Albert Van Zetten, met us at the Town Hall. He informed us that the "Old Ones" were once again "satisfied", and that their town would be safe for "the next ten thousand years until a new pact was to be formed."
        Officer Allen noticed a green glow coming from within the Town Hall. When asked, Mayor Van Zetten informed us a party sanctioned by the local police force was being conducted on the premises.
        The Mayor provided minutes from the most recent Town Hall meeting upon our request. The documents were written in an indistinguishable language, perhaps Chinese or another ethno-Asian dialect. We requested to speak with the Minutetaker in question, however Mayor Van Zetten was unable to comment.
        The Old Ones continue to rule this town.
    -> END
    
/* Good Ending */
=== goodending
    -   Police log, 29 September 2018 - Waxing Crescent.
    -   On September 29, 2018, I, Officer Allen and Officer Waite responded on a missing persons report in Launceston. Constable Williamson of the local police force briefed us and escorted us around the premises.  
        We found the town was deserted, bar Constable Williamson. As we patrolled the streets we did not see a single soul. The only sign of life in the town was a strange green glow from within the Town Hall. Myself and Officer Waite entered the Town Hall after radioing in our observations from our reconnaissance of the town.
        We could not trace the glow to a source - it seemed as if the air itself was emitting this uncanny luminescence. With some trepidation we continued further into the building. We found the the Council Chambers in complete disarray. Chairs were overturned, paintings were slashed and a smell like permeated the room. The meeting room table floated in the middle of the room turning slowly. We could not determine how it was suspended in the air.
        In the corner of the room underneath the minutetaker's table we found these Minutes of the recent Town Meetings. If what they say is true, something terrible has happened here...
    -> END

/* Best Ending */
=== bestending
    -   Police log, 29 September 2018 - Waning Gibbous.
    -   On September 29, 2018, I, Officer Allen and Officer Waite responded on a missing persons report in Launceston. We had been unable to raise Constable Williamson or any of our colleagues at the Launceston Police Department for several hours.
        On arrival in Launceston we found the town was deserted. As we patrolled the streets we did not see a single soul. The only sign of life in the town was a strange green glow from within the Town Hall. Myself and Officer Waite entered the Town Hall after radioing in our observations from our reconnaissance of the town.
        We could not trace the glow to a source - it seemed as if the air itself was emitting this uncanny luminescence. With some trepidation we continued further into the building. We found the the Council Chambers in complete disarray. Chairs were overturned, paintings were slashed and a smell like permeated the room. The meeting room table floated in the middle of the room turning slowly. We could not determine how it was suspended in the air.
        In the corner of the room underneath the minutetaker's table we found these Minutes of the recent Town Meetings. If what they say is true, something terrible has happened here...
    -> END
