/* 

GAME TITLE:
Occult Agenda 

DESCRIPTION:
There's something festering within the quaint Australian town of Pomeina, Tasmania. But, great news—you've been appointed the new town hall Minutetaker!

CREDITS:
Game concept & design: James McHugh, Justin Tam
Programming: James McHugh
Art: @wraith_ly
Writing: James McHugh, Justin Tam
Music & sound: Justin Tam

For InkJam 2018
Made with Ink by Inkle 
www.inklestudios.com 

*/

// Insanity state
VAR insanity = 0

// Ritual progress 
VAR ritualprogress = -> summonone

// Items
VAR dictionary = false
VAR fontpack = false
VAR photocopies = false
// VAR thesaurus = false 

// Character status 
VAR constabletaken = false
VAR mayortaken = false 

// Debug mode
VAR DEBUG = false
{DEBUG:
        \☣☣☣ DEBUG MODE ☣☣☣   
        *   [First Quarter...] -> meetingone
        *   [Waning Crescent...] -> meetingtwo
        *   [Full Moon...] -> meetingthree
        *   [All endings...] -> endinghandler
    - else:
        -> meetingone
}

/* Function for changing insanity */
=== function change(ref stat, value)
    ~stat = stat + value

EXTERNAL getTime()

=== function getTime() ===
// Usually external functions can only return placeholder
// results, otherwise they'd be defined in ink!
~ return "7:31pm"

/*                                      */ 
// GAME STARTS HERE

=== meetingone
#Filename=Poimena_Minutes_01Sep2018.doc #SetTime=7:01pm #SetDate=01/09/2018
#SetSeal=0
// Intro
    -   The following minutes were taken at the regular monthly meeting of the Mayor and the Town Board on 1 September 2018 at the Town Hall in 
        *   Poimena[], Tasmania. #Append
        Mayor Van Zetten called the meeting to order at 7:01pm - First Quarter.
    
    - (mayor)
        *   [Transcribe the Mayor's announcement]MAYOR'S ANNOUNCEMENTS:
            Mayor Van Zetten announced the passing of long time Poimena resident and beloved minutetaker, Bob Olmstead. 
            His second nephew, Robert Olmstead Jnr. took his place as the new minutetaker for the Town. The Mayor welcomed Robert back after his long time out of town in Launceston. He joked that Olmstead Snr. would always be inquiring into the Town Board's affairs. He advised the minutetaker to not be as inquisitive as his nosy uncle.
    
            Mayor Van Zetten opened the meeting by reminding residents that the annual renewal of the Town Pact would be lapsing next month. 
            The following meeting, taking place on 17 September, the Waning Crescent of the moon cycle, would be the date the conditions of the New Pact are decided upon. #AddMinutes=7
    
    -   (rollcallone)
        *   [Take attendance]ROLL CALL:
            Constable Jim Williamson, Constable with the Poimena Police Department, Ms. Alicia Marsh, and Mr. Barnabas Marsh (no relation), longtime resident of the Blue Tier Plateau, were present. 
            Mr. Marsh was unable to address his attendance verbally. Attendance noted. #AddMinutes=5
            **  [Clarify] Ms. Marsh reminded Minutetaker Olmstead that Mr. Marsh was not accustomed to speaking in English, and that he preferred just listening. 
                ~ change(insanity, 1)
            **  [Next item]
    
    -   (agendaone)
        The {first|second|third|final} item on the agenda was addressed by
        *   [The Mayor] Mayor Van Zetten, who addressed a complaint from the public about the town's sewerage system. The complaint was about the liquid that had been appearing in their home's faucets, which was said to cause sweats and mild hallucinations for anyone who drank the water.#Append #AddMinutes=3
            **  [Clarify] The Mayor explained that the "visitor" that had existed beneath the town for thousands (fact check?) of years had affected the quality of the town's water supply to be below an acceptable standard. He noted it had become considerably worse as the Pact's expiration date became closer.  
                ~ change(insanity, 1)
                *** [Clarify further] Ms. Marsh interrupted and reminded the meeting that there were other matters on the agenda.
                ~ change(insanity, 1)
                *** [Next item]  

            **  [Leave it] The Mayor moved to the next item. 
    
        *   Jim Williamson[]. Constable Williamson reported that he received complaints from homeowners that they had seen "unsettling" rubbish on their front lawn, left behind by young people with an out-of-town appearance. They reported #Append #AddMinutes=4
            **  [loud music]{insanity > 0: hearing a voice capable of sounds beyond the human throat coming from beneath the ground.}{insanity < 0: hearing loud music with foreign voices that sounded like singing. No further delinquent activity was reported.}#Append
                ~ change(insanity, -1)
                
            **  [the skin of small animals such as rabbits]{insanity > 0: seeing the skin of small animals such as rabbits, stained by dried blood, splayed out in runic patterns across their front lawns and emitting a stench like "singed bark".}{insanity < 0: "the  skin of small animals such as rabbits emitting a stench that smelled like "the new takeout place that opened up down the road last week."}#Append
                ~ change(insanity, 1)
                
                Upon further inspection he noted their gardens were dug up and 
                *** [graffiti]"graffiti" was found on the outside walls of their homes.#Append
                    ~ change(insanity, -1)
                    **** [Clarify] The Constable noted that the graffiti was in orange spraypaint. 
                    ~ change(insanity, -1)
                    **** [Next item]
                    
                *** [strange markings]"strange markings" were left against their garage doors{insanity > 0: arranged in words of a non-linear alphabet.}{insanity < 0:. The citizen noted the language was written in circular patterns, possibly Middle Eastern.}#Append
                    The Constable requested that if the old rituals are to be continued they be taken elsewhere. Constable Williamson added he "preferred the previous Old One(s)" because "they kept it clean, at least." 
                        ~ change(insanity, 1)
                    ****    [Clarify]Mayor Van Zetten reminded Minutetaker Olmstead that it was not his place to comment on agenda matters. Constable Williamson answered anyway, and said the markings were identified as an old language only communicated by visual, non-linear means. 
                        At this time, Mrs. Marsh pointed to the Constable in his chair and commented: "They are not pleased with you. You've spoken too freely."
                        ~ constabletaken = true
                    ****    [Next item]
                       
        *   Alicia Marsh[]. Ms. Marsh addressed the Mayor directly on the minute-keeping. She reminded the minutetaker that these should be recorded in formal hand.#Append #AddMinutes=6
            **  [Formal?] She said it was important for the sigils to be interpreted correctly.
                ~ change(insanity, 1)
                *** [Clarify] Constable Williamson suggested this matter be raised next meeting. All in favour. 
                ~ change(insanity, 1)
                *** [Next item]
        
            **  [Is shorthand allowed?]Ms. Marsh seemed frustrated. She reminded the minutetaker that shorthand did not exist in the time of the Old Ones.
                ~ change(insanity, 1)
                *** [Clarify] Mayor Van Zetten interrupted to say that this is how it always has been done, and reminded the minutetaker to not inquire into procedural matters. 
                ~ change(insanity, 1)
                *** [Next item]

        *   [Barnabas Marsh]
            the Mayor again. He asked Mr. Marsh if he had any motions to raise. Mr. Marsh did not comment - this was expected behaviour of Mr. Marsh at Town Hall meetings.#Append #AddMinutes=5
                
        *   [No one] #Delete
        ->  endagendaone
                
    -   {agendaone < 4:->  agendaone}{agendaone == 4:->  endagendaone}

- (endagendaone)
    {agendaone > 1:No further agenda items were raised.}
    At meeting close, Mayor Van Zetten provided Minutetaker Olmstead with an 'English-to-Old Language' dictionary to assist in recording the upcoming Pact Renewal negotiations. #AddMinutes=3
        ~ dictionary = true 
        The meeting was adjourned at {getTime()}.
        
        *   Meeting adjourned.
    -   #NextMeeting=meetingtwo
        -> DONE
        
=== meetingtwo
#Filename=WorshipTheOldOnes_13Sep2018.doc #SetTime=◬◭:◭◭pm #SetDate=Җѯ/ѳԓ/Ԇӂӂӭ
#SetSeal=1
    -   The following minutes were taken at the regular monthly meeting of the Mayor and the Town Board on 13 September 2018 at the Town Hall in Poimena, Tasmania. 
        Mayor Van Zetten called the meeting to order at 7:16pm - Waning Crescent. 
        He apologised for his lateness{insanity < 0:.}{insanity > 0:, as an Old One that had taken up residence in the fish pond in his backyard needed to be fed}.
        
        *   [Mayor's announcements] 

    MAYOR’S ANNOUNCEMENTS: 
    The Mayor repeated the conditions of the New Pact agreed upon in the previous meeting on 1 September.
    
    -   (rollcalltwo)
        *   [Take attendance]ROLL CALL:
            Ms. Alicia Marsh{not constabletaken:, Constable Jim Williamson,} and Mr. Barnabas Marsh were present. {constabletaken: Constable Jim Williamson sent his apologies; he was unable to make the meeting on account of the moon in its Waning Crescent phase.}

    -   (agendatwo)
        The {first|second|third|final} item on the agenda was addressed by
        *   [The Mayor] Mayor Van Zetten. He reminded the room that they would need to recite the terms of the New Pact in a language the Old Ones could understand. The Mayor said he would be handing out photocopies of additional sigil sheets needed for the ritual at the conclusion of the meeting. {insanity >0: When asked, the Mayor said the last copy of the dictionary went up in flames last time it was used.} #Append
            **  [Destroy dictionary] Ms. Marsh took the dictionary from Minutetaker Olmstead and asked that they move on from this agenda item.
                ~ dictionary = false
                *** [Clarify further] 
                    The Mayor seemed unable to speak, as if his mouth was glued together. There was fear in his eyes. 
                    Ms. Marsh clarified that the Old Ones were not happy with the Mayor. 
                    ~ mayortaken = true
                *** [Next item] 
                
            **  [Next item]
        
        *   Barnabas Marsh[]. Mr. Marsh was awake during this meeting, much to the surprise of the room.#Append
            Ms. Alicia Marsh acted as translator between the room and Mr. Marsh. (These Minutes record the translation as it was relayed by Ms. Marsh, however the original transcript in Old Language was not recorded).
            Barnabas stated he represented Pht'thya-l'y (sp?) as Visiting Consul, who sends their apologies for not attending the meeting for the last
            **  two meetings[]. He apologised again for his absence. #Append
                {insanity > 0: To Minutekeeper Olmstead, his voice sounded like tearing sheet metal.}
                ~ change(insanity, -1)
                
            **  ten thousand years[]. He reminded the room the Old Ones found English difficult as it was "too fast" and therefore indistinguishable. He stressed the importance of learning the language of the Old Ones in order for the New Pact to be renewed. #Append 
                {insanity > 0: Mr. Marsh levitated as he spoke.}
                ~ change(insanity, 1)
                
        *   Alicia Marsh[]. {not mayortaken: When prompted by the Mayor, }Mr. Marsh responded on her behalf however acting through Ms. Marsh as translator. It seemed as if when Ms. Marsh was translating the Old Language for Mr. Marsh, she was unable to speak for herself. Ms. Marsh's partner, who was in the audience, stood up and replied that "this was something she always did, no big deal."#Append
            ** [Clarify] Minutetaker Olmstead asked Ms. Marsh to clarify. 
                {not mayortaken: The Mayor asked Ms. Marsh about the}{mayortaken:Ms. Marsh clarified the} complaint that was made on 1 September, 1887 (NB. check date against records?). The issue pertained to the unavailability of the quality of {insanity > 0: raw cow }meat available in previous Pacts made. At the time, Ms. Marsh noted that 
                ***  the Pact required fresh meat[] for its success. #Append
                    Ms. Marsh did not comment on the matter. Motion to ensure fresh livestock available for Pact not passed, postponed until future meetings. 
                    ~ change(insanity, 1)
                
                *** they would be changing suppliers[] for their meat supply. #Append
                    Ms. Marsh did not comment further on the matter. 
                    ~ change(insanity, -2)

            ** [Next item]
        
        *   Minutetaker Olmstead[], who gave an update on the new font provided by the Old Ones. Olmstead said he tried to install the font pack however the file format was unrecognised by his laptop. {insanity > 0: He said that it caused his computer to start pulsing as if it was breathing.}#Append
            Mayor Van Zetten suggested Minutetaker Olmstead consult the Council's IT Department to raise an urgent ticket before next meeting. Ms. Marsh said it was important Minutetaker Olmstead was able to type in the Old Language for the New Pact to be successful.
            ~fontpack = true
            
        *   [No one] #Delete
            Mayor Van Zetten handed out photocopies of the runes to those in attendance. No further agenda items were raised. 
            ~ photocopies = true 
        ->  endagendatwo

    -   {agendatwo < 4:->  agendatwo}{agendatwo == 4:->  endagendatwo}

- (endagendatwo)
        *   Meeting adjourned.
        -   #NextMeeting=meetingthree
            -> DONE

// Full Moon phase 
=== meetingthree
#Filename=G☺D_HELP_US_ALL_G☺D_HELP_US_ALL_G☺D_HELP_US_ALL_G☺D_HELP_US_ALL_G☺D_HELP_US_ALL_G☺D_HELP_US_ALL.doc #SetTime=☺☺:☺☺pm #SetDate=☺☺/☺☺/☺☺☺☺
#SetSeal=2
    -   The following minutes were taken at the regular monthly meeting of the Mayor, the Town Board, {insanity > 0:and the Emissaries of Dagon} on 23 September 2018 at the Town Hall in Poimena, Tasmania. 
        {not mayortaken:Mayor Van Zetten}{mayortaken: Constable Williamson, standing in for Mayor Van Zetten, who was absent without reason given,}{mayortaken && constabletaken: Ms. Marsh, standing in for Mayor Van Zetten, who was absent without reason given,} called the meeting to order at 
        *   7.01pm[] - Full Moon.#Append
        
        *   11111.1111111pm[] - Full Moon.#Append
        
    -   -> startpact    
    
=== startpact
#SetSeal=3
        *   [Mayor's announcements]MAYOR'S ANNOUNCEMENTS:
            {not mayortaken:The Mayor}{mayortaken: Constable Williamson}{mayortaken && constabletaken: Ms. Marsh} announced that the New Pact would commence at today's meeting. 
            The opening address of the New Pact was spoken by {not constabletaken:Constable Williamson}{constabletaken:Ms. Marsh}, while the next phases were written down by Minutetaker Olmstead:

        *   [The Old One's announcements]THE OLD ONE'S ANNOUNCEMENTS: 
            The Old One spoke as if there was a voice present in the room with no clear source. It reminded the meeting that it was tired and it was time for the New Pact to claim its payment from "these feeble planeswalkers" once more.
            The opening address of the New Pact was spoken by {not constabletaken:Constable Williamson}{constabletaken: Ms. Marsh}, while the next phases were written down by Minutetaker Olmstead:
    -
    -> summonone

=== summonone
        *   \**REDACTED**
            The phrase was redacted by Minutetaker Olmstead of their own accord and not written down.
            ~ change(insanity, 1)
            -> ritualrefusal

        *   FONT_OLD Ph'nglui mglw'nafh Pht'thya-l'y Blue Tier Plateau wgah'nagl fhtagn[].
            ~ change(insanity, 1)
            // "In his house at Blue Tier Plateau dead Pht'thya-l'y lies dreaming"

        *   {dictionary && fontpack && photocopies}[In her house at Blue Tier Plateau dead Pht'thya-l'y lies in eternal nightmare.]IN HER HOUSE AT BLUE TIER PLATEAU DIED PHT'THYA-L'Y LIES IN ETERNAL NIGHTMARE.
            ~ change(insanity, -1)
            
    -
    ->  summontwo

=== summontwo
~ritualprogress = -> summontwo

        *   \(unintelligible)
            -> ritualrefusal

        *   {dictionary && fontpack && photocopies} [Pht'thya-l'y sleeps and waits, we rescind our prayers and silence it eternally.]PHT'THYA-L'Y SLEEPS AND WAITS, WE RESCIND OUR PRAYERS AND SILENCE IT ETERNALLY.            
            ~ change(insanity, -1)

        *   FONT_OLD Mglw'nafh fhthagn-ngah cf'ayak 'vulgtmm vugtlag'n[].
            ~ change(insanity, 1)
            // "[It] sleeps/waits and then acts, we send [our] prayers [to thee], answer [us]"
    
    -
    ->  summonthree
    
=== summonthree
~ritualprogress = -> summonthree

        *   {dictionary && fontpack && photocopies}[The Old Ones are forgotten by the people of Poimena. Pht'thya-l'y will sink to sleep eternal. Pht'thya-l'y is forgotten.]THE OLD ONES ARE FORGOTTEN BY THE PEOPLE OF POIMENA. PHT'THYA-L'Y WILL SINK TO SLEEP ETERNAL. PHT'THYA-L'Y IS FORGOTTEN.
            ~ change(insanity, -1)

        *   \(unintelligible)
            -> ritualrefusal

        *   FONT_OLD Ph'nglui mglw'nafh Pht'thya-l'y Poimena n'gha-ghaa naf'lthagn[].
            ~ change(insanity, 1)
            // DEATH "Gone but not forgotten, Pht'thya-l'y sleeps/waits at Poimena, [promising] death to one and all."
            
        *   {insanity > 6}The Old One Pht'thya-l'y anoints Minutetaker Olmstead as the new Prince of Poimena. 
            {not mayortaken: The Mayor gasped "A mere Minutetaker dares to challenge me? The impudence!}
            {mayortaken && constabletaken: Ms. Marsh laughed "I always knew an Olmstead had it in them!"}

    -   -> endinghandler
    
=== ritualrefusal
    * [Return to the ritual] -> mayorbridge
    * unintelligible
    The Mayor asked to check the notes.
        **[Show him] #Delete
        The Mayor told him to follow the ritual properly.
        **[Ignore] Olmstead ignored the Mayor.
    -
    //Ritual proceeds
    *  [Return to the ritual] -> mayorbridge
    * unintelligible
    The phrase was deemed unintelligible by Minutetaker Olmstead and therefore not written down.
    The Mayor uttered a deafening screech that was beyond any frequency imaginable to the human brain, and proceeded to roll his eyes into the back of his head as his arms writhed above his head.
        Mr. Marsh urged Minutetaker Olmstead to continue with the ritual before the Mayor traversed into a different plane.
            ~ change(insanity, 1)
    -
    //people realise you're messing it up
    * [last chance] -> mayorbridge
    * unintelligible
        The phrase was deemed unintelligible by Minutetaker Olmstead and therefore not written down.    Mr. Marsh leapt forth and seized Minutetaker Olmstead's laptoariaierklT$O444444444444t
            al4llllwtl44tttttttttttttttttttttttttttttttttt
            W4TLLT4LTL4W
            OOK0PK090O9KP9
            The Minutetaker seized control of the laptop once more and #EndBack=blood
            ~ change(insanity, 1)
            
        -> endinghandler
    
    - (mayorbridge)
    The Mayor asked Minutetaker Olmstead to continue transcribing the Pact. 
    -> ritualprogress
    
=== endinghandler
{   
    -   insanity <= 0 && not constabletaken && not mayortaken:
        #Ending=bestending
        -> DONE
        
    -   insanity <= 0 && not constabletaken || not mayortaken:
        #Ending=goodending
        -> DONE

    -   insanity > 0 && constabletaken && mayortaken:
        #Ending=worstending
        -> DONE
        
    -   insanity > 0 && constabletaken || mayortaken:
        #Ending=worstending
        -> DONE
        
    -   insanity > 6 && constabletaken && mayortaken:
        #Ending=evilending
        -> DONE
    
    -   else:
        #Ending=worstending
        -> DONE
}

/* Worst Ending */ 
=== worstending
#Filename=policelog_september29.doc
#SetSeal=0
    -   Police log, 29 September 2018 - Full Moon. 
    -   On September 29, 2018, I, Officer Allen and Officer Waite responded on an emergency 000 call from Poimena. We had been unable to raise Constable Williamson or any of our colleagues at the Poimena Police Department for several hours.
        Upon arrival we noted that there was no significant emergency in Poimena. The Mayor of the town, Albert Van Zetten, met us at the Town Hall. He informed us that the "Old Ones" were once again "satisfied", and that their town would be safe for "the next ten thousand years until a new pact was to be formed." 
        Officer Allen noticed a green glow coming from within the Town Hall. When asked, Mayor Van Zetten informed us a party sanctioned by the local police force was being conducted on the premises. 
        The Mayor provided minutes from the most recent Town Hall meeting upon our request. The documents were written in an indistinguishable language, perhaps Chinese or another ethno-Asian dialect. We requested to speak with Minutetaker Olmstead in question, however Mayor Van Zetten was unable to comment. He repeated one phrase, "It's not what you think," and walked off. 
        The Old Ones continue to rule this town. 
    -> END
    
/* Good Ending */
=== goodending 
#Filename=policelog_september29.doc
#SetSeal=0
    -   Police log, 29 September 2018 - Waxing Crescent.
    -   On September 29, 2018, I, Officer Allen and Officer Waite responded on a missing persons report in Poimena. Constable Williamson of the local police force briefed us and escorted us around the premises.  
        We found the town was deserted, bar Constable Williamson. As we patrolled the streets we did not see a single soul. The only sign of life in the town was a strange green glow from within the Town Hall. Myself and Officer Waite entered the Town Hall after radioing in our observations from our reconnaissance of the town.
        We could not trace the glow to a source - it seemed as if the air itself was emitting this uncanny luminescence. With some trepidation we continued further into the building. We found the the Council Chambers in complete disarray. Chairs were overturned, paintings were slashed and a smell like sulphur permeated the room. The meeting room table floated in the middle of the room turning slowly. We could not determine how it was suspended in the air.
        At this point, Constable Williamson remarked: "The ritual was completed, but they will come back for me." He trailed off and continued to utter the phrase as he paced the room. 
        In the corner of the room underneath the minutetaker's table we found these Minutes of the recent Town Meetings. 
        Scrawled at the based of the note, in pen: "It's not what you think." 
        If what they say is true, something terrible has happened here...
    -> END 

/* Best Ending */
=== bestending
#Filename=policelog_september29.doc
#SetSeal=0
    -   Police log, 29 September 2018 - Waning Gibbous. 
    -   On September 29, 2018, I, Officer Allen and Officer Waite responded on a missing persons report in Poimena. We had been unable to raise Constable Williamson or any of our colleagues at the Poimena Police Department for several hours.
        On arrival in Poimena we found the town was deserted. As we patrolled the streets we did not see a single soul. The only sign of life in the town was a strange green glow from within the Town Hall. Myself and Officer Waite entered the Town Hall after radioing in our observations from our reconnaissance of the town.
        We could not trace the glow to a source - it seemed as if the air itself was emitting this uncanny luminescence. With some trepidation we continued further into the building. We found the the Council Chambers in complete disarray. Chairs were overturned, paintings were slashed and a smell like sulphur permeated the room. The meeting room table floated in the middle of the room turning slowly. We could not determine how it was suspended in the air.
        In the corner of the room underneath the minutetaker's table we found these Minutes of the recent Town Meetings. 
        Scrawled at the based of the note, in pen: "It's not what you think." 
        If what they say is true, something terrible has happened here...
    -> END