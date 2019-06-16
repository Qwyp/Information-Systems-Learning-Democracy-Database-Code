using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Neo4j.Driver;
using Neo4j.Driver.V1;
using System.Collections.Generic;

namespace Synch_Mongo
{
    class Program
    {
        public static string MONGO_SERVER_ADDRESS = "mongodb://localhost:27017";
        public static string MONGO_DATABASE_NAME = "new_database";
        public static string MONGO_PEOPLE_COLLECTION_NAME = "PeopleAll";
        public static string MONGO_PARTIES_COLLECTION_NAME = "Parties";
        public static string MONGO_CONSTITUTION_COLLECTION_NAME = "Constitution";
        public static string MONGO_FOUNDATION_COLLECTION_NAME = "Foundation";
        public static string MONGO_INVOCATION_COLLECTION_NAME = "Invocation";
        public static string MONGO_ELECTIOIN_COLLECTION_NAME = "Election";
        public static string MONGO_REVOLUTION_COLLECTION_NAME = "Revolution";
        public static string NEO4J_DATABASE_ADDRESS = "bolt://localhost:7687";
        public static string NEO4J_USERNAME = "neo4j";
        public static string NEO4J_PASSSWORD = "123456";
        public static int PEOPLE_NODE_CREATOR_COUNTER = 0;
        public static int PARTIES_NODE_CREATOR_COUNTER = 0;
        public static int PEOPLE_PARTY_RELATION_COUNTER = 0;
        public static int PEOPLE_EVENT_RELATION_COUNTER = 0;
        public static int PARTY_EVNET_RELATION_COUNTER = 0;
        public static List<PeopleAll> PEOPLE_LIST;
        public static List<Parties> PARTIES_LIST;
        public static List<Constitution> CONSTITUTION_LIST;
        public static List<Foundation> FOUNDATION_LIST;
        public static List<Invocation> INVOCATION_LIST;
        public static List<Elections> ELECTIOIN_LIST;
        public static List<Revolution> REVOLUTION_LIST;
        public static String[] NEO4J_DEPLOY_QUERY = {
            "MERGE ( Democracy:Democracy { title: 'Evolution of Democracy',id:10001 	} )  MERGE (`Events10001`:Events {`id`: \"10001\"})  ON CREATE SET `Events10001`.`topicID` = 10001,  `Events10001`.`title` = \"Hambach Festival\",  `Events10001`.`description` = \"A rally began at Hambach Castle where participants demonstrated for the liberalization and unification of the German states.\", `Events10001`.`mongoId` = \"5cfb9f6a86186128f8a01a7b\" MERGE (`Events10002`:Events {`id`: \"10002\"}) ON CREATE SET `Events10002`.`topicID` = 10001,  `Events10002`.`title` = \"German Revolution\",  `Events10002`.`description` = \"German revolutions of 1848?49: An assembly in Mannheim adopted a resolution demanding a bill of rights.\", `Events10002`.`mongoId` = \"5cfba46086186128f8a01b3b\" MERGE (`Events10003`:Events {`id`: \"10003\"}) ON CREATE SET `Events10003`.`topicID` = 10001,  `Events10003`.`title` = \"November Revolution\",  `Events10003`.`description` = \"German Revolution of 1918?19: The Council of the People's Deputies, a body elected from the workers' councils of Berlin, introduced sweeping liberal reforms including the elimination of the Prussian three-class franchise and women's suffrage.\", `Events10003`.`mongoId` = \"5cfba65686186128f8a01b9a\" MERGE (`Events10004`:Events {`id`: \"10004\"}) ON CREATE SET `Events10004`.`topicID` = 10004,  `Events10004`.`title` = \"German Emergency act\",  `Events10004`.`description` = \"The German Emergency Acts were passed, amending the Basic Law for the Federal Republic of Germany to allow for the restriction of certain freedoms in the event of an emergency, and marking a major political defeat for the German student movement.\", `Events10004`.`mongoId` = \"5cfbca7c86186128f8a01d8d\" MERGE (`Events10005`:Events {`id`: \"10005\"}) ON CREATE SET `Events10005`.`topicID` = 10001,  `Events10005`.`title` = \"East German uprising\",  `Events10005`.`description` = \"Uprising of 1953 in East Germany: In response to a 10 percent increase in work quotas, between 60 and 80 construction workers went on strike in East Berlin. Their numbers quickly swelled and a general strike and protests were called for the next day.\", `Events10005`.`mongoId` = \"5cfbc73986186128f8a01ccd\" MERGE (`Events10006`:Events {`id`: \"10006\"}) ON CREATE SET `Events10006`.`topicID` = 10001,  `Events10006`.`title` = \"Monday Demonstration in east Germany\",  `Events10006`.`description` = \"Monday demonstrations in East Germany: A peaceful demonstration began in Leipzig, in East Germany, which called for democracy and the right of citizens to travel abroad.\", `Events10006`.`mongoId` = \"5cfbcbe986186128f8a01e02\" MERGE (`Events10007`:Events {`id`: \"10007\"}) ON CREATE SET `Events10007`.`topicID` = 10001,  `Events10007`.`title` = \"Treaty on the final settelment with respect to Germany\",  `Events10007`.`description` = \"The Treaty on the Final Settlement with Respect to Germany was signed by East and West Germany, the United States, the United Kingdom, France and the Soviet Union. The latter four renounced all rights they held in Germany.\", `Events10007`.`mongoId` = \"5cfbcdad86186128f8a01e98\" MERGE (`Events10008`:Events {`id`: \"10008\"}) ON CREATE SET `Events10008`.`topicID` = 10001,  `Events10008`.`title` = \"Kapp Putsch\",  `Events10008`.`description` = \"The Freikorps Marinebrigade Ehrhardt occupied Berlin. Wolfgang Kapp of the national conservative German National People's Party (DNVP) declared himself chancellor.\", `Events10008`.`mongoId` = \"5cfbc35886186128f8a01c39\" MERGE (`Events10009`:Events {`id`: \"10009\"}) ON CREATE SET `Events10009`.`topicID` = 10001,  `Events10009`.`title` = \"Bear Hall putsch\",  `Events10009`.`description` = \"Beer Hall Putsch: Nazi Party chairman Adolf Hitler led some six hundred Sturmabteilung (SA) to the B?rgerbr?ukeller in Munich, where they held Bavarian state officials Gustav Ritter von Kahr, Hans Ritter von Seisser and Otto von Lossow at gunpoint to demand they support a Nazi coup.\", `Events10009`.`mongoId` = \"5cfbc4e086186128f8a01c6f\" MERGE (`Events10014`:Events {`id`: \"10014\"}) ON CREATE SET `Events10014`.`topicID` = 10003,  `Events10014`.`title` = \"German worker association\",  `Events10014`.`description` = \"The General German Workers' Association was formed on 23rd May.\", `Events10014`.`mongoId` = \"5cfcf2f2f93d3a1592127201\" MERGE (`Events10015`:Events {`id`: \"10015\"}) ON CREATE SET `Events10015`.`topicID` = 10003,  `Events10015`.`title` = \"Failure of revolution\",  `Events10015`.`description` = \"The Revolution of 1848 failed in its attempt to unify the German-speaking states because the Frankfurt Assembly reflected the many different interests of the German ruling classes. Its members were unable to form coalitions and push for specific goals.\", `Events10015`.`mongoId` = \"5cfd0008f93d3a1592127565\" MERGE (`Events10016`:Events {`id`: \"10016\"}) ON CREATE SET `Events10016`.`topicID` = 10003,  `Events10016`.`title` = \"Pan German league\",  `Events10016`.`description` = \"The Pan-German League was a Pan-German nationalist organization which officially founded in 1891\", `Events10016`.`mongoId` = \"5cfd01cff93d3a159212763e\" MERGE (`Events10017`:Events {`id`: \"10017\"}) ON CREATE SET `Events10017`.`topicID` = 10003,  `Events10017`.`title` = \"Night of the long knives\",  `Events10017`.`description` = \"Night of the Long Knives: SS paramilitaries killed at least eighty-five potential threats to Hitler's power, including SA head Ernst R?hm and Gregor Strasser, head of the left wing of the Nazi Party.\", `Events10017`.`mongoId` = \"5cfd0427f93d3a1592127770\" MERGE (`Events10018`:Events {`id`: \"10018\"}) ON CREATE SET `Events10018`.`topicID` = 10003,  `Events10018`.`title` = \"The process of Gleichschaltung\",  `Events10018`.`description` = \"The process of Gleichschaltung, in which the government dismantled non-Nazi parties and societies, began.\", `Events10018`.`mongoId` = \"5cfd02d5f93d3a15921276b9\" MERGE (`Events10019`:Events {`id`: \"10019\"}) ON CREATE SET `Events10019`.`topicID` = 10003,  `Events10019`.`title` = \"The Dictatorship\",  `Events10019`.`description` = \"Hitler issued a law merging the powers of the presidency into the office of the chancellor.\", `Events10019`.`mongoId` = \"5cfd0549f93d3a1592127817\" MERGE (`Events10020`:Events {`id`: \"10020\"}) ON CREATE SET `Events10020`.`topicID` = 10003,  `Events10020`.`title` = \"Wannsee Confrence\",  `Events10020`.`description` = \"Wannsee Conference: A government conference was held to discuss the implementation of the Final Solution, the extermination of European Jewry.\", `Events10020`.`mongoId` = \"5cfd068ff93d3a15921278d0\" MERGE (`Events10021`:Events {`id`: \"10021\"}) ON CREATE SET `Events10021`.`topicID` = 10003,  `Events10021`.`title` = \"Postdam Confrence\",  `Events10021`.`description` = \"Potsdam Conference: British prime minister Clement Attlee, president Harry S. Truman of the United States and Joseph Stalin, the general secretary of the Soviet Communist Party, issued the Potsdam Agreement at Cecilienhof in Potsdam. The parties agreed that Germany would be returned to its 1937 borders with some additional cessions to the Soviet Union and ratified its division into British, French, American and Soviet occupation zones.\", `Events10021`.`mongoId` = \"5cfd0846f93d3a15921279f6\" MERGE (`Events10022`:Events {`id`: \"10022\"}) ON CREATE SET `Events10022`.`topicID` = 10003,  `Events10022`.`title` = \"Basic Treaty\",  `Events10022`.`description` = \"East and West Germany signed the Basic Treaty, in which each recognized the other's sovereignty.\", `Events10022`.`mongoId` = \"5cfd0a41f93d3a1592127b51\" MERGE (`Events10025`:Events {`id`: \"10025\"}) ON CREATE SET `Events10025`.`topicID` = 10003,  `Events10025`.`title` = \"Checkpoint at Berlin wall open\",  `Events10025`.`description` = \"The checkpoints on the Berlin Wall were opened.\", `Events10025`.`mongoId` = \"5cfd0c90f93d3a1592127cf8\" MERGE (`Events10026`:Events {`id`: \"10026\"}) ON CREATE SET `Events10026`.`topicID` = 10003,  `Events10026`.`title` = \"Occupation of the Ruhr\",  `Events10026`.`description` = \"Occupation of the Ruhr: France invaded the valley of the Ruhr.\", `Events10026`.`mongoId` = \"5cfd0e1df93d3a1592127e26\" MERGE (`Events10027`:Events {`id`: \"10027\"}) ON CREATE SET `Events10027`.`topicID` = 10003,  `Events10027`.`title` = \"Berlin wall\",  `Events10027`.`description` = \"Construction began on the Berlin Wall between East and West Berlin.\", `Events10027`.`mongoId` = \"5cfd0f56f93d3a1592127f29\" MERGE (`Events10028`:Events {`id`: \"10028\"}) ON CREATE SET `Events10028`.`topicID` = 10004,  `Events10028`.`title` = \"Weimar constitution\",  `Events10028`.`description` = \"The Constitution of the German Reich (German: Die Verfassung des Deutschen Reichs), usually known as the Weimar Constitution (Weimarer Verfassung) was the constitution that governed Germany during the Weimar Republic era\", `Events10028`.`mongoId` = \"5cfe1cca87783af44872f903\" MERGE (`Events10029`:Events {`id`: \"10029\"}) ON CREATE SET `Events10029`.`topicID` = 10004,  `Events10029`.`title` = \"Basic law celebration\",  `Events10029`.`description` = \"70 years of Basic Law are therefore a good reason to celebrate this milestone in German history, the celebration of the constitution on May 22 focuses on the bright and dark sides of the German legal system\", `Events10029`.`mongoId` = \"5cfe20dd87783af44872f932\"  MERGE (`Events10030`:Events {`id`: \"10030\"}) ON CREATE SET `Events10030`.`topicID` = 10005,  `Events10030`.`title` = \"East Germany foundation\",  `Events10030`.`description` = \"East Germany, officially the German Democratic Republic, was a country that existed from 1949 to 1990, when the eastern portion of Germany was part of the Eastern Bloc during the Cold War.\", `Events10030`.`mongoId` = \"5cfd10bef93d3a1592128088\" MERGE (`Events10031`:Events {`id`: \"10031\"}) ON CREATE SET `Events10031`.`topicID` = 10005,  `Events10031`.`title` = \"West Germnay foundation\",  `Events10031`.`description` = \"West Germany was the informal name for the Federal Republic of Germany from 1949 to 1990, a period referred to by historians as the Bonn Republic, an era when the western portion of Germany was part of the Western bloc during the Cold War.\", `Events10031`.`mongoId` = \"5cfd1249f93d3a15921281fd\" MERGE (`Events10032`:Events {`id`: \"10032\"}) ON CREATE SET `Events10032`.`topicID` = 10002,  `Events10032`.`title` = \"1949 German federal election\",  `Events10032`.`description` = \"Schumacher had explicitly refused a grand coalition and led his party into opposition, where it would remain until December 1966 assuming the chair of the SPD parliamentary group as minority leader. On 12 September 1949, he lost the german presidential election , defeated by FDP chairman in the second ballot. Schumacher died on 20 August 1952 of the long-term consequences of his concentration camp imprisonment during the Nazi years. Adenauer had favored the formation of a smaller Centre-right coalition from the beginning. \", `Events10032`.`mongoId` = \"5cfe262d87783af44872f9a2\" MERGE (`Events10033`:Events {`id`: \"10033\"}) ON CREATE SET `Events10033`.`topicID` = 10002,  `Events10033`.`title` = \"1871 German federal election\",  `Events10033`.`description` = \"Beginning in 1871, launched the Kulturkampf (?cultural struggle?), a campaign in concert with German liberals against political Catholicism. Liberals saw the Roman Catholic church as politically reactionary and feared the appeal of a clerical party to the more than one-third of Germans who professed Roman Catholicism.\", `Events10033`.`mongoId` = \"5cfe5f2d87783af44872fea0\" MERGE (`Events10034`:Events {`id`: \"10034\"}) ON CREATE SET `Events10034`.`topicID` = 10002,  `Events10034`.`title` = \"1932 German federal elections\",  `Events10034`.`description` = \"Despite achieving a much better result than in the November 1932 election, the Nazis did not do as well as Hitler had hoped. In spite of massive violence and voter intimidation, the Nazis won only 43.9% of the vote, rather than the majority that he had expected.\", `Events10034`.`mongoId` = \"5cfe658d87783af44872ff31\" MERGE (`Timelines10001`:Timelines {`id`: 10001}) ON CREATE SET `Timelines10001`.`title` = \"Monarchy\" MERGE (`Timelines10002`:Timelines {`id`: 10002}) ON CREATE SET `Timelines10002`.`title` = \"weimarer republic (Democracy)\" MERGE (`Timelines10003`:Timelines {`id`: 10003}) ON CREATE SET `Timelines10003`.`title` = \"Dictatorship\" MERGE (`Timelines10004`:Timelines {`id`: 10004}) ON CREATE SET `Timelines10004`.`title` = \"Allied Occupation of Germany\" MERGE (`Timelines10005`:Timelines {`id`: 10005}) ON CREATE SET `Timelines10005`.`title` = \"Western Germany (BRD Democracy)\" MERGE (`Timelines10006`:Timelines {`id`: 10006}) ON CREATE SET `Timelines10006`.`title` = \"Eastern Germany (DDR Socialist Dictatorship)\" MERGE (`Timelines10007`:Timelines {`id`: 10007}) ON CREATE SET `Timelines10007`.`title` = \"United Germany (BRD Democracy) \" MERGE (`Topics10001`:Topics {`id`: 10001}) ON CREATE SET `Topics10001`.`title` = \"Revolution\" MERGE (`Topics10002`:Topics {`id`: 10002}) ON CREATE SET `Topics10002`.`title` = \"Elections\" MERGE (`Topics10003`:Topics {`id`: 10003}) ON CREATE SET `Topics10003`.`title` = \"Invocation\" MERGE (`Topics10004`:Topics {`id`: 10004}) ON CREATE SET `Topics10004`.`title` = \"Constitution\" MERGE (`Topics10005`:Topics {`id`: 10005}) ON CREATE SET `Topics10005`.`title` = \"Foundation\" MERGE (`years10001`:years {`id`: \"10001\"}) ON CREATE SET `years10001`.`years` = 1832 MERGE (`years10002`:years {`id`: \"10002\"}) ON CREATE SET `years10002`.`years` = 1833 MERGE (`years10003`:years {`id`: \"10003\"}) ON CREATE SET `years10003`.`years` = 1834 MERGE (`years10004`:years {`id`: \"10004\"}) ON CREATE SET `years10004`.`years` = 1835 MERGE (`years10005`:years {`id`: \"10005\"}) ON CREATE SET `years10005`.`years` = 1836 MERGE (`years10006`:years {`id`: \"10006\"}) ON CREATE SET `years10006`.`years` = 1837 MERGE (`years10007`:years {`id`: \"10007\"}) ON CREATE SET `years10007`.`years` = 1838 MERGE (`years10008`:years {`id`: \"10008\"}) ON CREATE SET `years10008`.`years` = 1839 MERGE (`years10009`:years {`id`: \"10009\"}) ON CREATE SET `years10009`.`years` = 1840 MERGE (`years10010`:years {`id`: \"10010\"}) ON CREATE SET `years10010`.`years` = 1841 MERGE (`years10011`:years {`id`: \"10011\"}) ON CREATE SET `years10011`.`years` = 1842 MERGE (`years10012`:years {`id`: \"10012\"}) ON CREATE SET `years10012`.`years` = 1843 MERGE (`years10013`:years {`id`: \"10013\"}) ON CREATE SET `years10013`.`years` = 1844 MERGE (`years10014`:years {`id`: \"10014\"}) ON CREATE SET `years10014`.`years` = 1845 MERGE (`years10015`:years {`id`: \"10015\"}) ON CREATE SET `years10015`.`years` = 1846 MERGE (`years10016`:years {`id`: \"10016\"}) ON CREATE SET `years10016`.`years` = 1847 MERGE (`years10017`:years {`id`: \"10017\"}) ON CREATE SET `years10017`.`years` = 1848 MERGE (`years10018`:years {`id`: \"10018\"}) ON CREATE SET `years10018`.`years` = 1849 MERGE (`years10019`:years {`id`: \"10019\"}) ON CREATE SET `years10019`.`years` = 1850 MERGE (`years10020`:years {`id`: \"10020\"}) ON CREATE SET `years10020`.`years` = 1851 MERGE (`years10021`:years {`id`: \"10021\"}) ON CREATE SET `years10021`.`years` = 1852 MERGE (`years10022`:years {`id`: \"10022\"}) ON CREATE SET `years10022`.`years` = 1853 MERGE (`years10023`:years {`id`: \"10023\"}) ON CREATE SET `years10023`.`years` = 1854 MERGE (`years10024`:years {`id`: \"10024\"}) ON CREATE SET `years10024`.`years` = 1855 MERGE (`years10025`:years {`id`: \"10025\"}) ON CREATE SET `years10025`.`years` = 1856 MERGE (`years10026`:years {`id`: \"10026\"}) ON CREATE SET `years10026`.`years` = 1857 MERGE (`years10027`:years {`id`: \"10027\"}) ON CREATE SET `years10027`.`years` = 1858 MERGE (`years10028`:years {`id`: \"10028\"}) ON CREATE SET `years10028`.`years` = 1859 MERGE (`years10029`:years {`id`: \"10029\"}) ON CREATE SET `years10029`.`years` = 1860 MERGE (`years10030`:years {`id`: \"10030\"}) ON CREATE SET `years10030`.`years` = 1861 MERGE (`years10031`:years {`id`: \"10031\"}) ON CREATE SET `years10031`.`years` = 1862 MERGE (`years10032`:years {`id`: \"10032\"}) ON CREATE SET `years10032`.`years` = 1863 MERGE (`years10033`:years {`id`: \"10033\"}) ON CREATE SET `years10033`.`years` = 1864 MERGE (`years10034`:years {`id`: \"10034\"}) ON CREATE SET `years10034`.`years` = 1865 MERGE (`years10035`:years {`id`: \"10035\"}) ON CREATE SET `years10035`.`years` = 1866 MERGE (`years10036`:years {`id`: \"10036\"}) ON CREATE SET `years10036`.`years` = 1867 MERGE (`years10037`:years {`id`: \"10037\"}) ON CREATE SET `years10037`.`years` = 1868 MERGE (`years10038`:years {`id`: \"10038\"}) ON CREATE SET `years10038`.`years` = 1869 MERGE (`years10039`:years {`id`: \"10039\"}) ON CREATE SET `years10039`.`years` = 1870 MERGE (`years10040`:years {`id`: \"10040\"}) ON CREATE SET `years10040`.`years` = 1871 MERGE (`years10041`:years {`id`: \"10041\"}) ON CREATE SET `years10041`.`years` = 1872 MERGE (`years10042`:years {`id`: \"10042\"}) ON CREATE SET `years10042`.`years` = 1873 MERGE (`years10043`:years {`id`: \"10043\"}) ON CREATE SET `years10043`.`years` = 1874 MERGE (`years10044`:years {`id`: \"10044\"}) ON CREATE SET `years10044`.`years` = 1875 MERGE (`years10045`:years {`id`: \"10045\"}) ON CREATE SET `years10045`.`years` = 1876 MERGE (`years10046`:years {`id`: \"10046\"}) ON CREATE SET `years10046`.`years` = 1877 MERGE (`years10047`:years {`id`: \"10047\"}) ON CREATE SET `years10047`.`years` = 1878 MERGE (`years10048`:years {`id`: \"10048\"}) ON CREATE SET `years10048`.`years` = 1879 MERGE (`years10049`:years {`id`: \"10049\"}) ON CREATE SET `years10049`.`years` = 1880 MERGE (`years10050`:years {`id`: \"10050\"}) ON CREATE SET `years10050`.`years` = 1881 MERGE (`years10051`:years {`id`: \"10051\"}) ON CREATE SET `years10051`.`years` = 1882 MERGE (`years10052`:years {`id`: \"10052\"}) ON CREATE SET `years10052`.`years` = 1883 MERGE (`years10053`:years {`id`: \"10053\"}) ON CREATE SET `years10053`.`years` = 1884 MERGE (`years10054`:years {`id`: \"10054\"}) ON CREATE SET `years10054`.`years` = 1885 MERGE (`years10055`:years {`id`: \"10055\"}) ON CREATE SET `years10055`.`years` = 1886 MERGE (`years10056`:years {`id`: \"10056\"}) ON CREATE SET `years10056`.`years` = 1887 MERGE (`years10057`:years {`id`: \"10057\"}) ON CREATE SET `years10057`.`years` = 1888 MERGE (`years10058`:years {`id`: \"10058\"}) ON CREATE SET `years10058`.`years` = 1889 MERGE (`years10059`:years {`id`: \"10059\"}) ON CREATE SET `years10059`.`years` = 1890 MERGE (`years10060`:years {`id`: \"10060\"}) ON CREATE SET `years10060`.`years` = 1891 MERGE (`years10061`:years {`id`: \"10061\"}) ON CREATE SET `years10061`.`years` = 1892 MERGE (`years10062`:years {`id`: \"10062\"}) ON CREATE SET `years10062`.`years` = 1893 MERGE (`years10063`:years {`id`: \"10063\"}) ON CREATE SET `years10063`.`years` = 1894 MERGE (`years10064`:years {`id`: \"10064\"}) ON CREATE SET `years10064`.`years` = 1895 MERGE (`years10065`:years {`id`: \"10065\"}) ON CREATE SET `years10065`.`years` = 1896  MERGE (`years10066`:years {`id`: \"10066\"}) ON CREATE SET `years10066`.`years` = 1897 MERGE (`years10067`:years {`id`: \"10067\"}) ON CREATE SET `years10067`.`years` = 1898 MERGE (`years10068`:years {`id`: \"10068\"}) ON CREATE SET `years10068`.`years` = 1899 MERGE (`years10069`:years {`id`: \"10069\"}) ON CREATE SET `years10069`.`years` = 1900 MERGE (`years10070`:years {`id`: \"10070\"}) ON CREATE SET `years10070`.`years` = 1901 MERGE (`years10071`:years {`id`: \"10071\"}) ON CREATE SET `years10071`.`years` = 1902 MERGE (`years10072`:years {`id`: \"10072\"}) ON CREATE SET `years10072`.`years` = 1903 MERGE (`years10073`:years {`id`: \"10073\"}) ON CREATE SET `years10073`.`years` = 1904 MERGE (`years10074`:years {`id`: \"10074\"}) ON CREATE SET `years10074`.`years` = 1905 MERGE (`years10075`:years {`id`: \"10075\"}) ON CREATE SET `years10075`.`years` = 1906 MERGE (`years10076`:years {`id`: \"10076\"}) ON CREATE SET `years10076`.`years` = 1907 MERGE (`years10077`:years {`id`: \"10077\"}) ON CREATE SET `years10077`.`years` = 1908 MERGE (`years10078`:years {`id`: \"10078\"}) ON CREATE SET `years10078`.`years` = 1909 MERGE (`years10079`:years {`id`: \"10079\"}) ON CREATE SET `years10079`.`years` = 1910 MERGE (`years10080`:years {`id`: \"10080\"}) ON CREATE SET `years10080`.`years` = 1911 MERGE (`years10081`:years {`id`: \"10081\"}) ON CREATE SET `years10081`.`years` = 1912 MERGE (`years10082`:years {`id`: \"10082\"}) ON CREATE SET `years10082`.`years` = 1913 MERGE (`years10083`:years {`id`: \"10083\"}) ON CREATE SET `years10083`.`years` = 1914 MERGE (`years10084`:years {`id`: \"10084\"}) ON CREATE SET `years10084`.`years` = 1915 MERGE (`years10085`:years {`id`: \"10085\"}) ON CREATE SET `years10085`.`years` = 1916 MERGE (`years10086`:years {`id`: \"10086\"}) ON CREATE SET `years10086`.`years` = 1917 MERGE (`years10087`:years {`id`: \"10087\"}) ON CREATE SET `years10087`.`years` = 1918 MERGE (`years10088`:years {`id`: \"10088\"}) ON CREATE SET `years10088`.`years` = 1919 MERGE (`years10089`:years {`id`: \"10089\"}) ON CREATE SET `years10089`.`years` = 1920 MERGE (`years10090`:years {`id`: \"10090\"}) ON CREATE SET `years10090`.`years` = 1921 MERGE (`years10091`:years {`id`: \"10091\"}) ON CREATE SET `years10091`.`years` = 1922 MERGE (`years10092`:years {`id`: \"10092\"}) ON CREATE SET `years10092`.`years` = 1923 MERGE (`years10093`:years {`id`: \"10093\"}) ON CREATE SET `years10093`.`years` = 1924 MERGE (`years10094`:years {`id`: \"10094\"}) ON CREATE SET `years10094`.`years` = 1925 MERGE (`years10095`:years {`id`: \"10095\"}) ON CREATE SET `years10095`.`years` = 1926 MERGE (`years10096`:years {`id`: \"10096\"}) ON CREATE SET `years10096`.`years` = 1927 MERGE (`years10097`:years {`id`: \"10097\"}) ON CREATE SET `years10097`.`years` = 1928 MERGE (`years10098`:years {`id`: \"10098\"}) ON CREATE SET `years10098`.`years` = 1929 MERGE (`years10099`:years {`id`: \"10099\"}) ON CREATE SET `years10099`.`years` = 1930 MERGE (`years10100`:years {`id`: \"10100\"}) ON CREATE SET `years10100`.`years` = 1931 MERGE (`years10101`:years {`id`: \"10101\"}) ON CREATE SET `years10101`.`years` = 1932 MERGE (`years10102`:years {`id`: \"10102\"}) ON CREATE SET `years10102`.`years` = 1933 MERGE (`years10103`:years {`id`: \"10103\"}) ON CREATE SET `years10103`.`years` = 1934 MERGE (`years10104`:years {`id`: \"10104\"}) ON CREATE SET `years10104`.`years` = 1935 MERGE (`years10105`:years {`id`: \"10105\"}) ON CREATE SET `years10105`.`years` = 1936 MERGE (`years10106`:years {`id`: \"10106\"}) ON CREATE SET `years10106`.`years` = 1937 MERGE (`years10107`:years {`id`: \"10107\"}) ON CREATE SET `years10107`.`years` = 1938 MERGE (`years10108`:years {`id`: \"10108\"}) ON CREATE SET `years10108`.`years` = 1939 MERGE (`years10109`:years {`id`: \"10109\"}) ON CREATE SET `years10109`.`years` = 1940 MERGE (`years10110`:years {`id`: \"10110\"}) ON CREATE SET `years10110`.`years` = 1941 MERGE (`years10111`:years {`id`: \"10111\"}) ON CREATE SET `years10111`.`years` = 1942 MERGE (`years10112`:years {`id`: \"10112\"}) ON CREATE SET `years10112`.`years` = 1943 MERGE (`years10113`:years {`id`: \"10113\"}) ON CREATE SET `years10113`.`years` = 1944 MERGE (`years10114`:years {`id`: \"10114\"}) ON CREATE SET `years10114`.`years` = 1945 MERGE (`years10115`:years {`id`: \"10115\"}) ON CREATE SET `years10115`.`years` = 1946 MERGE (`years10116`:years {`id`: \"10116\"}) ON CREATE SET `years10116`.`years` = 1947 MERGE (`years10117`:years {`id`: \"10117\"}) ON CREATE SET `years10117`.`years` = 1948 MERGE (`years10118`:years {`id`: \"10118\"}) ON CREATE SET `years10118`.`years` = 1949 MERGE (`years10119`:years {`id`: \"10119\"}) ON CREATE SET `years10119`.`years` = 1950 MERGE (`years10120`:years {`id`: \"10120\"}) ON CREATE SET `years10120`.`years` = 1951 MERGE (`years10121`:years {`id`: \"10121\"}) ON CREATE SET `years10121`.`years` = 1952 MERGE (`years10122`:years {`id`: \"10122\"}) ON CREATE SET `years10122`.`years` = 1953 MERGE (`years10123`:years {`id`: \"10123\"}) ON CREATE SET `years10123`.`years` = 1954 MERGE (`years10124`:years {`id`: \"10124\"}) ON CREATE SET `years10124`.`years` = 1955 MERGE (`years10125`:years {`id`: \"10125\"}) ON CREATE SET `years10125`.`years` = 1956 MERGE (`years10126`:years {`id`: \"10126\"}) ON CREATE SET `years10126`.`years` = 1957 MERGE (`years10127`:years {`id`: \"10127\"}) ON CREATE SET `years10127`.`years` = 1958 MERGE (`years10128`:years {`id`: \"10128\"}) ON CREATE SET `years10128`.`years` = 1959 MERGE (`years10129`:years {`id`: \"10129\"}) ON CREATE SET `years10129`.`years` = 1960 MERGE (`years10130`:years {`id`: \"10130\"}) ON CREATE SET `years10130`.`years` = 1961 MERGE (`years10131`:years {`id`: \"10131\"}) ON CREATE SET `years10131`.`years` = 1962 MERGE (`years10132`:years {`id`: \"10132\"}) ON CREATE SET `years10132`.`years` = 1963 MERGE (`years10133`:years {`id`: \"10133\"}) ON CREATE SET `years10133`.`years` = 1964 MERGE (`years10134`:years {`id`: \"10134\"}) ON CREATE SET `years10134`.`years` = 1965 MERGE (`years10135`:years {`id`: \"10135\"}) ON CREATE SET `years10135`.`years` = 1966 MERGE (`years10136`:years {`id`: \"10136\"}) ON CREATE SET `years10136`.`years` = 1967 MERGE (`years10137`:years {`id`: \"10137\"}) ON CREATE SET `years10137`.`years` = 1968 MERGE (`years10138`:years {`id`: \"10138\"}) ON CREATE SET `years10138`.`years` = 1969 MERGE (`years10139`:years {`id`: \"10139\"}) ON CREATE SET `years10139`.`years` = 1970 MERGE (`years10140`:years {`id`: \"10140\"}) ON CREATE SET `years10140`.`years` = 1971 MERGE (`years10141`:years {`id`: \"10141\"}) ON CREATE SET `years10141`.`years` = 1972 MERGE (`years10142`:years {`id`: \"10142\"}) ON CREATE SET `years10142`.`years` = 1973 MERGE (`years10143`:years {`id`: \"10143\"}) ON CREATE SET `years10143`.`years` = 1974 MERGE (`years10144`:years {`id`: \"10144\"}) ON CREATE SET `years10144`.`years` = 1975 MERGE (`years10145`:years {`id`: \"10145\"}) ON CREATE SET `years10145`.`years` = 1976 MERGE (`years10146`:years {`id`: \"10146\"}) ON CREATE SET `years10146`.`years` = 1977 MERGE (`years10147`:years {`id`: \"10147\"}) ON CREATE SET `years10147`.`years` = 1978 MERGE (`years10148`:years {`id`: \"10148\"}) ON CREATE SET `years10148`.`years` = 1979 MERGE (`years10149`:years {`id`: \"10149\"}) ON CREATE SET `years10149`.`years` = 1980 MERGE (`years10150`:years {`id`: \"10150\"}) ON CREATE SET `years10150`.`years` = 1981 MERGE (`years10151`:years {`id`: \"10151\"}) ON CREATE SET `years10151`.`years` = 1982 MERGE (`years10152`:years {`id`: \"10152\"}) ON CREATE SET `years10152`.`years` = 1983 MERGE (`years10153`:years {`id`: \"10153\"}) ON CREATE SET `years10153`.`years` = 1984 MERGE (`years10154`:years {`id`: \"10154\"}) ON CREATE SET `years10154`.`years` = 1985 MERGE (`years10155`:years {`id`: \"10155\"}) ON CREATE SET `years10155`.`years` = 1986 MERGE (`years10156`:years {`id`: \"10156\"}) ON CREATE SET `years10156`.`years` = 1987 MERGE (`years10157`:years {`id`: \"10157\"}) ON CREATE SET `years10157`.`years` = 1988 MERGE (`years10158`:years {`id`: \"10158\"}) ON CREATE SET `years10158`.`years` = 1989 MERGE (`years10159`:years {`id`: \"10159\"}) ON CREATE SET `years10159`.`years` = 1990 MERGE (`years10160`:years {`id`: \"10160\"}) ON CREATE SET `years10160`.`years` = 1991 MERGE (`years10161`:years {`id`: \"10161\"}) ON CREATE SET `years10161`.`years` = 1992 MERGE (`years10162`:years {`id`: \"10162\"}) ON CREATE SET `years10162`.`years` = 1993 MERGE (`years10163`:years {`id`: \"10163\"}) ON CREATE SET `years10163`.`years` = 1994 MERGE (`years10164`:years {`id`: \"10164\"}) ON CREATE SET `years10164`.`years` = 1995 MERGE (`years10165`:years {`id`: \"10165\"}) ON CREATE SET `years10165`.`years` = 1996 MERGE (`years10166`:years {`id`: \"10166\"}) ON CREATE SET `years10166`.`years` = 1997 MERGE (`years10167`:years {`id`: \"10167\"}) ON CREATE SET `years10167`.`years` = 1998 MERGE (`years10168`:years {`id`: \"10168\"}) ON CREATE SET `years10168`.`years` = 1999 MERGE (`years10169`:years {`id`: \"10169\"}) ON CREATE SET `years10169`.`years` = 2000 MERGE (`years10170`:years {`id`: \"10170\"}) ON CREATE SET `years10170`.`years` = 2001 MERGE (`years10171`:years {`id`: \"10171\"}) ON CREATE SET `years10171`.`years` = 2002 MERGE (`years10172`:years {`id`: \"10172\"}) ON CREATE SET `years10172`.`years` = 2003 MERGE (`years10173`:years {`id`: \"10173\"}) ON CREATE SET `years10173`.`years` = 2004 MERGE (`years10174`:years {`id`: \"10174\"}) ON CREATE SET `years10174`.`years` = 2005 MERGE (`years10175`:years {`id`: \"10175\"}) ON CREATE SET `years10175`.`years` = 2006 MERGE (`years10176`:years {`id`: \"10176\"}) ON CREATE SET `years10176`.`years` = 2007 MERGE (`years10177`:years {`id`: \"10177\"}) ON CREATE SET `years10177`.`years` = 2008 MERGE (`years10178`:years {`id`: \"10178\"}) ON CREATE SET `years10178`.`years` = 2009 MERGE (`years10179`:years {`id`: \"10179\"}) ON CREATE SET `years10179`.`years` = 2010 MERGE (`years10180`:years {`id`: \"10180\"}) ON CREATE SET `years10180`.`years` = 2011 MERGE (`years10181`:years {`id`: \"10181\"}) ON CREATE SET `years10181`.`years` = 2012 MERGE (`years10182`:years {`id`: \"10182\"}) ON CREATE SET `years10182`.`years` = 2013 MERGE (`years10183`:years {`id`: \"10183\"}) ON CREATE SET `years10183`.`years` = 2014 MERGE (`years10184`:years {`id`: \"10184\"}) ON CREATE SET `years10184`.`years` = 2015 MERGE (`years10185`:years {`id`: \"10185\"}) ON CREATE SET `years10185`.`years` = 2016  MERGE (`years10186`:years {`id`: \"10186\"}) ON CREATE SET `years10186`.`years` = 2017 MERGE (`years10187`:years {`id`: \"10187\"}) ON CREATE SET `years10187`.`years` = 2018 MERGE (`years10188`:years {`id`: \"10188\"}) ON CREATE SET `years10188`.`years` = 2019;",
            "CREATE CONSTRAINT ON (E:Events) ASSERT E.id IS UNIQUE;",
            "CREATE CONSTRAINT ON (T:Timelines) ASSERT T.id IS UNIQUE;",
            "CREATE CONSTRAINT ON (y:years) ASSERT y.id IS UNIQUE;",
            "CREATE CONSTRAINT ON (E:Events) ASSERT E.mongoId IS UNIQUE;",
            "CREATE CONSTRAINT ON (P:Person) ASSERT P.mongoId IS UNIQUE;",
            "CREATE CONSTRAINT ON (P:Party) ASSERT P.mongoId IS UNIQUE;",
            "CREATE CONSTRAINT ON ()-[rel:PARTICIPATE_IN]-() ASSERT exists(rel.role);",
            "MATCH (e:Events) SET e.id = toInteger(e.id);",
            "MATCH (y:years) SET y.id = toInteger(y.id);",
            "MATCH (d:Democracy),(t:Topics) MERGE (t)-[:TOPIC_OF]->(d);",
            "MATCH (t:Topics),(e:Events) WHERE t.id = e.topicID MERGE (e)<-[:HAS_EVENT]-(t);",
            "MATCH (y:years),(yy:years) WHERE toInteger(y.years) = toInteger(yy.years)+1 MERGE (yy)-[:FOLLOWS]->(y);",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10001 AND y.years = 1832 MERGE (t)-[:STARTED_IN]->(y);",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10001 AND y.years = 1918 MERGE (t)-[:ENDED_IN]->(y);",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10002 AND y.years = 1919 MERGE (t)-[:STARTED_IN]->(y);",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10002 AND y.years = 1933 MERGE (t)-[:ENDED_IN]->(y)",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10003 AND y.years = 1933 MERGE (t)-[:STARTED_IN]->(y);",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10003 AND y.years = 1945 MERGE (t)-[:ENDED_IN]->(y)",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10004 AND y.years = 1945 MERGE (t)-[:STARTED_IN]->(y);",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10004 AND y.years = 1952 MERGE (t)-[:ENDED_IN]->(y)",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10005 AND y.years = 1949 MERGE (t)-[:STARTED_IN]->(y);",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10005 AND y.years = 1990 MERGE (t)-[:ENDED_IN]->(y);",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10006 AND y.years = 1949 MERGE (t)-[:STARTED_IN]->(y);",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10006 AND y.years = 1989 MERGE (t)-[:ENDED_IN]->(y);",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10007 AND y.years = 1990 MERGE (t)-[:STARTED_IN]->(y);",
            "MATCH (t:Timelines),(y:years) WHERE t.id = 10007 AND y.years = 2019 MERGE (t)-[:ENDED_IN]->(y);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10001 AND y.years = 1832 MERGE (e)-[:EVENT_START {`startdate`: date('1832-05-27')}]->(y)<-[:EVENT_END {`endtdate`: date('1832-05-27')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10002 AND y.years = 1848 MERGE (e)-[:EVENT_START {`startdate`: date('1848-02-27')}]->(y);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10002 AND y.years = 1849 MERGE (e)-[:EVENT_END {`endtdate`: date('1849-06-18')}]->(y);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10003 AND y.years = 1918 MERGE (e)-[:EVENT_START {`startdate`: date('1918-11-09')}]->(y);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10003 AND y.years = 1919 MERGE (e)-[:EVENT_END {`endtdate`: date('1919-08-11')}]->(y);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10004 AND y.years = 1968 MERGE (e)-[:EVENT_START {`startdate`: date('1968-05-30')}]->(y)<-[:EVENT_END {`endtdate`: date('1968-05-30')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10005 AND y.years = 1953 MERGE (e)-[:EVENT_START {`startdate`: date('1953-06-16')}]->(y)<-[:EVENT_END {`endtdate`: date('1953-06-16')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10006 AND y.years = 1989 MERGE (e)-[:EVENT_START {`startdate`: date('1989-09-04')}]->(y);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10006 AND y.years = 1991 MERGE (e)-[:EVENT_END {`endtdate`: date('1991-04-22')}]->(y);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10007 AND y.years = 1990 MERGE (e)-[:EVENT_START {`startdate`: date('1990-09-12')}]->(y)<-[:EVENT_END {`endtdate`: date('1990-09-12')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10008 AND y.years = 1920 MERGE (e)-[:EVENT_START {`startdate`: date('1920-03-13')}]->(y)<-[:EVENT_END {`endtdate`: date('1920-03-13')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10009 AND y.years = 1923 MERGE (e)-[:EVENT_START {`startdate`: date('1923-11-08')}]->(y)<-[:EVENT_END {`endtdate`: date('1923-11-08')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10014 AND y.years = 1863 MERGE (e)-[:EVENT_START {`startdate`: date('1863-05-23')}]->(y)<-[:EVENT_END {`endtdate`: date('1863-05-23')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10015 AND y.years = 1849 MERGE (e)-[:EVENT_START {`startdate`: date('1849-06-18')}]->(y)<-[:EVENT_END {`endtdate`: date('1849-06-18')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10016 AND y.years = 1891 MERGE (e)-[:EVENT_START {`startdate`: date('1891-04-09')}]->(y)<-[:EVENT_END {`endtdate`: date('1891-04-09')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10017 AND y.years = 1934 MERGE (e)-[:EVENT_START {`startdate`: date('1934-06-30')}]->(y)<-[:EVENT_END {`endtdate`: date('1934-06-30')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10018 AND y.years = 1933 MERGE (e)-[:EVENT_START {`startdate`: date('1933-01-30')}]->(y)<-[:EVENT_END {`endtdate`: date('1933-01-30')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10019 AND y.years = 1934 MERGE (e)-[:EVENT_START {`startdate`: date('1934-08-01')}]->(y);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10019 AND y.years = 1945 MERGE (e)-[:EVENT_END {`endtdate`: date('1945-04-30')}]->(y);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10020 AND y.years = 1942 MERGE (e)-[:EVENT_START {`startdate`: date('1942-01-20')}]->(y)<-[:EVENT_END {`endtdate`: date('1942-01-20')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10021 AND y.years = 1945 MERGE (e)-[:EVENT_START {`startdate`: date('1945-08-01')}]->(y)<-[:EVENT_END {`endtdate`: date('1945-08-01')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10022 AND y.years = 1972 MERGE (e)-[:EVENT_START {`startdate`: date('1972-12-21')}]->(y)<-[:EVENT_END {`endtdate`: date('1972-12-21')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10025 AND y.years = 1989 MERGE (e)-[:EVENT_START {`startdate`: date('1989-11-09')}]->(y)<-[:EVENT_END {`endtdate`: date('1989-11-09')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10026 AND y.years = 1923 MERGE (e)-[:EVENT_START {`startdate`: date('1923-01-11')}]->(y)",
            "MATCH (e:Events),(y:years) WHERE e.id = 10026 AND y.years = 1925 MERGE (e)-[:EVENT_END {`endtdate`: date('1925-08-25')}]->(y);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10027 AND y.years = 1961 MERGE (e)-[:EVENT_START {`startdate`: date('1961-08-13')}]->(y);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10027 AND y.years = 1989 MERGE (e)-[:EVENT_END {`endtdate`: date('1989-11-09')}]->(y);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10028 AND y.years = 1919 MERGE (e)-[:EVENT_START {`startdate`: date('1919-08-11')}]->(y)<-[:EVENT_END {`endtdate`: date('1919-08-11')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10029 AND y.years = 2019 MERGE (e)-[:EVENT_START {`startdate`: date('2019-05-22')}]->(y)<-[:EVENT_END {`endtdate`: date('2019-05-22')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10030 AND y.years = 1949 MERGE (e)-[:EVENT_START {`startdate`: date('1949-10-07')}]->(y)<-[:EVENT_END {`endtdate`: date('1949-10-07')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10031 AND y.years = 1949 MERGE (e)-[:EVENT_START {`startdate`: date('1949-05-23')}]->(y)<-[:EVENT_END {`endtdate`: date('1949-05-23')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10032 AND y.years = 1949 MERGE (e)-[:EVENT_START {`startdate`: date('1949-08-14')}]->(y)<-[:EVENT_END {`endtdate`: date('1949-08-14')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10033 AND y.years = 1871 MERGE (e)-[:EVENT_START {`startdate`: date('1871-03-21')}]->(y)<-[:EVENT_END {`endtdate`: date('1871-03-21')}]-(e);",
            "MATCH (e:Events),(y:years) WHERE e.id = 10034 AND y.years = 1932 MERGE (e)-[:EVENT_START {`startdate`: date('1949-07-31')}]->(y)<-[:EVENT_END {`endtdate`: date('1949-07-31')}]-(e);"
        };



        static void Main(string[] args)
        {
            Console.WriteLine("Run Neo4j server and MongoDB server...\n");
            Console.WriteLine("Please Enter Neo4j credential to connect");
            Console.WriteLine("****************************************\n");
            Console.Write("Username :");
            NEO4J_USERNAME = Console.ReadLine();
            Console.Write("Password :");
            NEO4J_PASSSWORD = Console.ReadLine();

            MergingMongoAndNeo.databaseDeployment();
            Program.peopleCreation();
            Program.partiesCreation();
            Program.relationPeopleParties();
            Console.WriteLine("\nDone!!!\nYour Neo4J Database has been successfully deployed and connected with MongoDB...\nPress any Key to Exit...");
            Console.ReadKey();
        }

        public static void peopleCreation()
        {
            Console.WriteLine("\nConnecting to MongoDB...");
            var client = new MongoClient(MONGO_SERVER_ADDRESS);

            var database = client.GetDatabase(MONGO_DATABASE_NAME);

            var PeopleCollection = database.GetCollection<PeopleAll>(MONGO_PEOPLE_COLLECTION_NAME);
            Console.WriteLine("\nGetting People Collectoin...");


            var peopleList = PeopleCollection.Find(people => people.Id != null)
                .SortBy(sorting => sorting.Name)
                .ToListAsync()
                .Result;

            PEOPLE_LIST = peopleList;
            Console.WriteLine("Creating People Nodes :");
            foreach (var person in peopleList)
            {
                MergingMongoAndNeo.createPeopleNodes(person.Id, person.Name);
                Console.Write("#");

            }
            Console.WriteLine("\n" + PEOPLE_NODE_CREATOR_COUNTER + " Nodes With label of People and 2 Property has been Created\n\n");
        }

        public static void partiesCreation()
        {
            var client = new MongoClient(MONGO_SERVER_ADDRESS);

            var database = client.GetDatabase(MONGO_DATABASE_NAME);

            var PartiesCollection = database.GetCollection<Parties>(MONGO_PARTIES_COLLECTION_NAME);

            Console.WriteLine("Getting Parties Collectoin...");
            var partiesList = PartiesCollection.Find(parties => parties.Id != null)
                .SortBy(sorting => sorting.Political_Party_Name.full_Name)
                .ToListAsync()
                .Result;

            PARTIES_LIST = partiesList;
            Console.WriteLine("Creating Parties Nodes :");
            foreach (var party in partiesList)
            {
                MergingMongoAndNeo.createPartiesNodes(party.Id, party.Political_Party_Name.full_Name, party.Political_Party_Name.Abbreviation);
                Console.Write("#");

            }
            Console.WriteLine("\n" + PARTIES_NODE_CREATOR_COUNTER + " Nodes With label of Party and 3 Property has been Created\n\n");
        }

        public static void relationPeopleParties()
        {
            var client = new MongoClient(MONGO_SERVER_ADDRESS);

            var database = client.GetDatabase(MONGO_DATABASE_NAME);

            var constitutionCollection = database.GetCollection<Constitution>(MONGO_CONSTITUTION_COLLECTION_NAME);

            Console.WriteLine("Getting Constitution Collectoin...");
            var constitutionList = constitutionCollection.Find(parties => parties.Id != null)
                .SortBy(sorting => sorting.Id)
                .ToListAsync()
                .Result;
            CONSTITUTION_LIST = constitutionList;

            var invocationCollection = database.GetCollection<Invocation>(MONGO_INVOCATION_COLLECTION_NAME);

            Console.WriteLine("Getting Invocation Collectoin...");
            var invocationList = invocationCollection.Find(parties => parties.Id != null)
                .SortBy(sorting => sorting.Id)
                .ToListAsync()
                .Result;
            INVOCATION_LIST = invocationList;

            var foundationCollection = database.GetCollection<Foundation>(MONGO_FOUNDATION_COLLECTION_NAME);

            Console.WriteLine("Getting Foundation Collectoin...");
            var foundationList = foundationCollection.Find(parties => parties.Id != null)
                .SortBy(sorting => sorting.Id)
                .ToListAsync()
                .Result;
            FOUNDATION_LIST = foundationList;

            var revolutionCollection = database.GetCollection<Revolution>(MONGO_REVOLUTION_COLLECTION_NAME);

            Console.WriteLine("Getting Revolution Collectoin...");
            var revolutionList = revolutionCollection.Find(parties => parties.Id != null)
                .SortBy(sorting => sorting.Id)
                .ToListAsync()
                .Result;
            REVOLUTION_LIST = revolutionList;

            var electionCollection = database.GetCollection<Elections>(MONGO_ELECTIOIN_COLLECTION_NAME);

            Console.WriteLine("Getting Election Collectoin...\n");
            var electionnList = electionCollection.Find(parties => parties.Id != null)
                .SortBy(sorting => sorting.Id)
                .ToListAsync()
                .Result;
            ELECTIOIN_LIST = electionnList;

            Console.WriteLine("Creating People Relations with Events :");
            foreach (var evnt in CONSTITUTION_LIST)
            {
                if (evnt.People.Length > 0)
                {
                    for (int i = 0; i < evnt.People.Length; i++)
                    {
                        MergingMongoAndNeo.createPeopleEventRelation(evnt.People[i], evnt.Id);
                        Console.Write("#");
                    }
                }
            }
            foreach (var evnt in REVOLUTION_LIST)
            {
                if (evnt.People.Length > 0)
                {
                    for (int i = 0; i < evnt.People.Length; i++)
                    {
                        MergingMongoAndNeo.createPeopleEventRelation(evnt.People[i], evnt.Id);
                        Console.Write("#");
                    }
                }
            }
            foreach (var evnt in INVOCATION_LIST)
            {
                if (evnt.People.Length > 0)
                {
                    for (int i = 0; i < evnt.People.Length; i++)
                    {
                        MergingMongoAndNeo.createPeopleEventRelation(evnt.People[i], evnt.Id);
                        Console.Write("#");
                    }
                }
            }
            foreach (var evnt in FOUNDATION_LIST)
            {
                if (evnt.People.Length > 0)
                {
                    for (int i = 0; i < evnt.People.Length; i++)
                    {
                        MergingMongoAndNeo.createPeopleEventRelation(evnt.People[i], evnt.Id);
                        Console.Write("#");
                    }
                }
            }
            foreach (var evnt in ELECTIOIN_LIST)
            {
                if (evnt.People.Length > 0)
                {
                    for (int i = 0; i < evnt.People.Length; i++)
                    {
                        MergingMongoAndNeo.createPeopleEventRelation(evnt.People[i], evnt.Id);
                        Console.Write("#");
                    }
                }
            }
            Console.WriteLine("\n" + PEOPLE_EVENT_RELATION_COUNTER + " Relation with label of PARTICIPATE_IN and 1 property has been Created\n\n");


            Console.WriteLine("Creating political Parties Relations with Events :");
            foreach (var evnt in CONSTITUTION_LIST)
            {
                if (evnt.Political_Parties.Length > 0)
                {
                    for (int i = 0; i < evnt.Political_Parties.Length; i++)
                    {
                        MergingMongoAndNeo.createPartyEventRelation(evnt.Political_Parties[i], evnt.Id);
                        Console.Write("#");
                    }
                }
            }
            foreach (var evnt in REVOLUTION_LIST)
            {
                if (evnt.Political_Parties.Length > 0)
                {
                    for (int i = 0; i < evnt.Political_Parties.Length; i++)
                    {
                        MergingMongoAndNeo.createPartyEventRelation(evnt.Political_Parties[i], evnt.Id);
                        Console.Write("#");
                    }
                }
            }
            foreach (var evnt in INVOCATION_LIST)
            {
                if (evnt.Political_Parties.Length > 0)
                {
                    for (int i = 0; i < evnt.Political_Parties.Length; i++)
                    {
                        MergingMongoAndNeo.createPartyEventRelation(evnt.Political_Parties[i], evnt.Id);
                        Console.Write("#");
                    }
                }
            }
            foreach (var evnt in FOUNDATION_LIST)
            {
                if (evnt.Political_Parties.Length > 0)
                {
                    for (int i = 0; i < evnt.Political_Parties.Length; i++)
                    {
                        MergingMongoAndNeo.createPartyEventRelation(evnt.Political_Parties[i], evnt.Id);
                        Console.Write("#");
                    }
                }
            }
            foreach (var evnt in ELECTIOIN_LIST)
            {
                if (evnt.Political_Parties.Length > 0)
                {
                    int counter = 0;
                    ObjectId winner = evnt.Id;
                    for (int i = 0; i < evnt.Political_Parties.Length; i++)
                    {

                        if (evnt.Political_Parties[i].seats > counter)
                        {
                            counter = evnt.Political_Parties[i].seats;
                            winner = evnt.Political_Parties[i].party;

                        }
                        MergingMongoAndNeo.createPartyEventRelation(evnt.Political_Parties[i].party, evnt.Id);
                        Console.Write("#");
                    }
                    MergingMongoAndNeo.createElectionWinnerRelation(winner, evnt.Id);
                    Console.Write("#");
                }
            }
            Console.WriteLine("\n" + PARTY_EVNET_RELATION_COUNTER + " Relation with label of ENVOLVED_IN has been Created\n3 Relation with label of HAS_WON has been Created\n\n");



            Console.WriteLine("Creating People Relations with political Parties :");

            foreach (var person in PEOPLE_LIST)
            {
                if (person.Parties.Length > 0)
                {
                    for (int i = 0; i < person.Parties.Length; i++)
                    {
                        MergingMongoAndNeo.createPeoplePartyRelation(person.Id, person.Parties[i]);
                        Console.Write("#");
                    }
                }
            }
            Console.WriteLine("\n" + PEOPLE_PARTY_RELATION_COUNTER + " Relation with label of MEMBER_OF and 1 property has been Created");

        }


        public class Parties
        {
            public ObjectId Id { get; set; }
            public Political_Party_Name Political_Party_Name { get; set; }
            public string Agenda { get; set; }
            public ObjectId[] People { get; set; }
        }

        public class Political_Party_Name
        {
            public string Abbreviation { get; set; }
            public string full_Name { get; set; }
        }



        public class PeopleAll
        {
            public ObjectId Id { get; set; }
            public string Name { get; set; }
            public string Info { get; set; }
            public string Agenda { get; set; }
            public string BirthDeath { get; set; }
            public ObjectId[] Image { get; set; }
            public ObjectId[] Parties { get; set; }
        }


        public class Constitution
        {
            public ObjectId Id { get; set; }
            public string Title { get; set; }
            public ObjectId[] People { get; set; }
            public Detail Detail { get; set; }
            public string Aftermath { get; set; }
            public string Living_Condition { get; set; }
            public string Rule_Set { get; set; }
            public ObjectId[] Political_Parties { get; set; }
        }

        public class Detail
        {
            public string Text { get; set; }
            public ObjectId[] Video { get; set; }
            public ObjectId[] Images { get; set; }
        }


        public class Elections
        {
            public ObjectId Id { get; set; }
            public string Title { get; set; }
            public Detail Detail { get; set; }
            public ObjectId[] People { get; set; }
            public Political_Parties[] Political_Parties { get; set; }
            public string Attendees { get; set; }
            public string Rule_Set { get; set; }
            public string Aftermath { get; set; }
            public string Living_Condition { get; set; }
        }


        public class Political_Parties
        {
            public ObjectId party { get; set; }
            public int seats { get; set; }
        }


        public class Foundation
        {
            public ObjectId Id { get; set; }
            public string Title { get; set; }
            public ObjectId[] People { get; set; }
            public Detail Detail { get; set; }
            public string Aftermath { get; set; }
            public string Living_Condition { get; set; }
            public string Rule_Set { get; set; }
            public ObjectId[] Political_Parties { get; set; }
        }



        public class Revolution
        {
            public ObjectId Id { get; set; }
            public string Title { get; set; }
            public ObjectId[] People { get; set; }
            public Detail Detail { get; set; }
            public string Aftermath { get; set; }
            public string Living_Condition { get; set; }
            public string Rule_Set { get; set; }
            public ObjectId[] Political_Parties { get; set; }
        }


        public class Invocation
        {
            public ObjectId Id { get; set; }
            public string Title { get; set; }
            public ObjectId[] People { get; set; }
            public Detail Detail { get; set; }
            public string Aftermath { get; set; }
            public string Living_Condition { get; set; }
            public string Rule_Set { get; set; }
            public ObjectId[] Political_Parties { get; set; }
        }








        public class MergingMongoAndNeo : IDisposable
        {
            private readonly IDriver _driver;

            public MergingMongoAndNeo(string uri, string user, string password)
            {
                _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
            }

            public static void createPeopleNodes(ObjectId mongoID, string name)
            {
                using (var greeter = new MergingMongoAndNeo(NEO4J_DATABASE_ADDRESS, NEO4J_USERNAME, NEO4J_PASSSWORD))
                {
                    greeter.peopleNodeMaker(mongoID, name);
                }
            }

            public void peopleNodeMaker(ObjectId mongoID, string name)
            {
                using (var session = _driver.Session())
                {
                    var greeting = session.WriteTransaction(tx =>
                    {
                        var result = tx.Run("MERGE (p:Person {name: \"" + name + "\", mongoId: \"" + mongoID + "\"})");
                        var texting = "Successful";
                        return texting;
                    });
                    PEOPLE_NODE_CREATOR_COUNTER++;
                }
            }

            public void Dispose()
            {
                _driver?.Dispose();
            }


            public static void createPartiesNodes(ObjectId mongoID, string name, string abbr)
            {
                using (var greeter = new MergingMongoAndNeo(NEO4J_DATABASE_ADDRESS, NEO4J_USERNAME, NEO4J_PASSSWORD))
                {
                    greeter.partiesNodeMaker(mongoID, name, abbr);
                }
            }

            public void partiesNodeMaker(ObjectId mongoID, string name, string abbr)
            {
                using (var session = _driver.Session())
                {
                    var greeting = session.WriteTransaction(tx =>
                    {
                        var result = tx.Run("MERGE (p:Party {name: \"" + name + "\",abbreviation: \"" + abbr + "\", mongoId: \"" + mongoID + "\"})");
                        var texting = "Successful";
                        return texting;
                    });
                    PARTIES_NODE_CREATOR_COUNTER++;
                }
            }

            public static void createPeoplePartyRelation(ObjectId personID, ObjectId partyID)
            {
                using (var greeter = new MergingMongoAndNeo(NEO4J_DATABASE_ADDRESS, NEO4J_USERNAME, NEO4J_PASSSWORD))
                {
                    greeter.peoplePartyRelationMaker(personID, partyID);
                }
            }

            public void peoplePartyRelationMaker(ObjectId personID, ObjectId partyID)
            {
                using (var session = _driver.Session())
                {
                    var greeting = session.WriteTransaction(tx =>
                    {
                        var result = tx.Run("MATCH (party:Party),(person:Person) WHERE party.mongoId = \"" + partyID + "\" AND person.mongoId = \"" + personID + "\" MERGE (person)-[:MEMBER_OF]->(party)");
                        var texting = "Successful";
                        return texting;
                    });
                    PEOPLE_PARTY_RELATION_COUNTER++;
                }
            }


            public static void databaseDeployment()
            {
                using (var greeter = new MergingMongoAndNeo(NEO4J_DATABASE_ADDRESS, NEO4J_USERNAME, NEO4J_PASSSWORD))
                {
                    greeter.startDB();
                }
            }

            public void startDB()
            {
                using (var session = _driver.Session())
                {
                    Console.WriteLine("\nDeploying Neo4J DataBase...\nPlease Wait...\n");
                    foreach (var query in NEO4J_DEPLOY_QUERY)
                    {
                        var greeting = session.WriteTransaction(tx =>
                        {
                            var result = tx.Run(query);
                            var texting = "Success";
                            return texting;
                        });
                    }
                    Console.WriteLine("Neo4J DataBase Successfully Deployed\n\n");
                }
            }


            public static void createPeopleEventRelation(ObjectId personID, ObjectId eventID)
            {
                using (var greeter = new MergingMongoAndNeo(NEO4J_DATABASE_ADDRESS, NEO4J_USERNAME, NEO4J_PASSSWORD))
                {
                    greeter.peopleEventRelationMaker(personID, eventID);
                }
            }

            public void peopleEventRelationMaker(ObjectId personID, ObjectId eventID)
            {
                using (var session = _driver.Session())
                {
                    var greeting = session.WriteTransaction(tx =>
                    {
                        var result = tx.Run("MATCH (event:Events),(person:Person) WHERE event.mongoId = \"" + eventID + "\" AND person.mongoId = \"" + personID + "\" MERGE (person)-[:PARTICIPATE_IN {role: \"Member\"}]->(event)");
                        var texting = "Successful";
                        return texting;
                    });
                    PEOPLE_EVENT_RELATION_COUNTER++;
                }
            }

            public static void createPartyEventRelation(ObjectId partyID, ObjectId eventID)
            {
                using (var greeter = new MergingMongoAndNeo(NEO4J_DATABASE_ADDRESS, NEO4J_USERNAME, NEO4J_PASSSWORD))
                {
                    greeter.partyEventRelationMaker(partyID, eventID);
                }
            }

            public void partyEventRelationMaker(ObjectId partyID, ObjectId eventID)
            {
                using (var session = _driver.Session())
                {
                    var greeting = session.WriteTransaction(tx =>
                    {
                        var result = tx.Run("MATCH (event:Events),(party:Party) WHERE event.mongoId = \"" + eventID + "\" AND party.mongoId = \"" + partyID + "\" MERGE (party)-[:ENVOLVED_IN]->(event)");
                        var texting = "Successful";
                        return texting;
                    });
                    PARTY_EVNET_RELATION_COUNTER++;
                }
            }

            public static void createElectionWinnerRelation(ObjectId partyID, ObjectId eventID)
            {
                using (var greeter = new MergingMongoAndNeo(NEO4J_DATABASE_ADDRESS, NEO4J_USERNAME, NEO4J_PASSSWORD))
                {
                    greeter.electionWinnerRelationMaker(partyID, eventID);
                }
            }

            public void electionWinnerRelationMaker(ObjectId partyID, ObjectId eventID)
            {
                using (var session = _driver.Session())
                {
                    var greeting = session.WriteTransaction(tx =>
                    {
                        var result = tx.Run("MATCH (event:Events),(party:Party) WHERE event.mongoId = \"" + eventID + "\" AND party.mongoId = \"" + partyID + "\" MERGE (party)-[:HAS_WON]->(event)");
                        var texting = "Successful";
                        return texting;
                    });
                }
            }

        }

    }


}
