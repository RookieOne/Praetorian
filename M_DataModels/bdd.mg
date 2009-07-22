// MisBehaveGrammar ver 0.1 (Oslo CTP, PDC  build)
// Author: Claudio Perrone (cperrone AT innerworkings DOT com)
module InnerWorkings
{
    @{CaseSensitive[false]}
    language MisBehave
    {
        syntax Main = s:List(Story) => id("MisBehave.Stories") { valuesof(s)};
                
        syntax Story = sthead:StoryHeader body:StoryNarrative sc:Scenarios?
            =>  id("Story") { sthead, valuesof(body), id("Scenarios") {valuesof(sc)} };

        syntax Scenarios = l:List(Scenario) => {valuesof(l)};

        syntax Scenario = ScenarioHeader ScenarioNarrative;
        nest syntax StoryHeader
            = StoryPrefix t:StoryTitle LineEnd
            => id("Title")[t]; 

        syntax StoryNarrative = 
            AsStep IWantStep SoThatStep 
            | InOrderToStep AsStep IWantStep;

        syntax ScenarioNarrative =
            g:GivenStepsSequence wt:List(WhenThenStepsSequence)
            => Body[g, valuesof(wt)];
            
        syntax WhenThenStepsSequence 
            = w:WhenStepsSequence t:ThenStepsSequence
            => WhenThen[w, t];

        syntax GivenStepsSequence 
            = g:GivenStep a:AndSteps
            => id("Given") [valuesof(g), valuesof(a)];

        nest syntax GivenStep
            = Given c:Context LineEnd
            =>[c];

        syntax AndSteps 
            = a:List(AndStep) => [valuesof(a)]
            | empty; 
            
        nest syntax AndStep
            = And c:TextFragment LineEnd 
            =>c;

        syntax WhenStepsSequence 
            = w:WhenStep a:AndSteps
            => id("When") [valuesof(w), valuesof(a)];

        nest syntax WhenStep
            = When e:Event LineEnd
            => [e];

        nest syntax ScenarioHeader
            = ScenarioPrefix t:ScenarioTitle LineEnd
            => id("Title")[t];

        syntax ThenStepsSequence 
            = t:ThenStep a:AndSteps
            => id("Then") [valuesof(t), valuesof(a)];

        nest syntax ThenStep
            = Then e:Outcome LineEnd
            => [e];


        nest syntax InOrderToStep
            = InOrderTo n:Need LineEnd
            =>id("Need") [n];
                 
        nest syntax AsStep 
            = As r:Role LineEnd
            => id("Role") [r];

        nest syntax IWantStep 
            = IWant f:Feature LineEnd
            => id("Feature") [f];

        nest syntax SoThatStep
            = SoThat n:Need LineEnd
            => id("Need") [n];

        @{Classification["Keyword"]} token StoryPrefix = ("Story:" | "Feature:");   
        @{Classification["Keyword"]} token InOrderTo = "In order to" " "+;
        @{Classification["Keyword"]} token As = "As" " "+;
        @{Classification["Keyword"]} token IWant = "I want" " to"? " "+ ;
        @{Classification["Keyword"]} token SoThat = "So that" " "+;

        @{Classification["Keyword"]} token ScenarioPrefix = "Scenario:";   
        @{Classification["Keyword"]} token Given = "Given" " "+;
        @{Classification["Keyword"]} token GivenScenario = ("GivenScenario"|"Given Scenario") " "+;
        @{Classification["Keyword"]} token When = "When" " "+;
        @{Classification["Keyword"]} token Then = "Then" " "+;
        @{Classification["Keyword"]} token And = "And" " "+;
        
        token ScenarioTitle = TextFragment;
        token StoryTitle = TextFragment;
        token Context = TextFragment;
        token Event = TextFragment;
        token Outcome = TextFragment;

        token Role = TextFragment;
        token Feature = TextFragment;
        token Need = TextFragment;
        
        token TextFragment = ^("\r" | "\n")+;
        token LineEnd = ("\r" | "\n")+;
        
     
        syntax List(Element) 
            = e:Element =>  {e} 
            | l:List(Element) e:Element => { valuesof(l), e };

        interleave Skippable = " " | "\r" | "\n" | Comment;
        interleave Comment = CommentToken;
        
        @{Classification["Comment"]}
        token CommentToken 
            = CommentDelimited
            | CommentLine;
        token CommentDelimited = "/*" CommentDelimitedContent* "*/";
        token CommentDelimitedContent = 
            ^('*')
            | '*'  ^('/');
        token CommentLine = "//" CommentLineContent*;
                token CommentLineContent = ^(
                 '\u000A' // New Line
              |  '\u000D' // Carriage Return
              |  '\u0085' // Next Line
              |  '\u2028' // Line Separator
              |  '\u2029'); // Paragraph Separator
        
    }
}