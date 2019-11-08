using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class AbbreviateSample
    {
        public Int32 T;
        public List<String> A;
        public List<String> B;
    }




    class Abbreviate : TProblem<AbbreviateSample, List<String>>
    {
        private Random rnd;

        public Abbreviate()
        {
            rnd = new Random();
            Solution s1 = new Solution(Solution1);
            Solution s2 = new Solution(Solution2);
            Solution s3 = new Solution(Solution3);
            Solution s4 = new Solution(Solution4);

            Solutions.Add(s1);
            Solutions.Add(s2);
            //Solutions.Add(s3);
            Solutions.Add(s4);
        }

        public override void GenSamples()
        {
            Samples.Add(new AbbreviateSample() { T = 1, A = new List<string>() { "daBcd" }, B = new List<string>() { "ABC" } });
            Answers.Add(new List<string>() { "YES" });

            /*
            Samples.Add(new AbbreviateSample()
            {
                T = 10,
                A = new List<string>() { "Pi", "AfPZN", "LDJAN", "UMKFW", "KXzQ", "LIT", "QYCH", "DFIQG", "sYOCa", "JHMWY" },
                B = new List<string>() { "P", "APZNC", "LJJM", "UMKFW", "K", "LIT", "QYCH", "DFIQG", "YOCN", "HUVPW" }
            });
            Answers.Add(new List<string>()
            {
                "YES","NO","NO","YES","NO","YES","YES","YES","NO","NO"
            });
            */

            Samples.Add(new AbbreviateSample()
            {
                T = 3,
                A = new List<string>() { "AbCdE", "beFgH", "beFgH" },
                B = new List<string>() { "AFE", "EFG", "EFH" }
            });

            Answers.Add(new List<string>()
            {
                "NO", "NO", "YES"
            });

            /*
            Samples.Add(new AbbreviateSample()
            {
                T = 1,
                A = new List<string>() { "NmFafsdfsdsdfsdfIUsdfsdYQWNEFNsdfVdfgsdfFfFIWsdfERKHSDFasdSDKLFgdfsdfgSDFLsjDFKLSDasdfgdfgdfgdfJFJKSDHFKasdfJSDFH" },
                B = new List<string>() { "NMFIUYQWNEFNVFIWERKHSDFSDKLFSDFLSJDFKLSDJFJKSDHFKJSDFH" }
            });

            Answers.Add(new List<string>()
            {
                "NO"
            });

            Samples.Add(new AbbreviateSample()
            {
                T = 1,
                A = new List<string>() { "ASLKDJASdfgdfgdfgdfgdfgdfgdfgdfgdfgfdgKLDJKLJLGHKLGFHDFJGKLfghFSDJGLDFKGJDFKLGJLDFKGJDLFKGJAKPJDGHALJKGHKJHWERJKHWEJKRHWKEJRHNJWEKJNRKJWENRJKWENRKWJERNKWEJRNKWEJRNWKEJRNWKEJRNWKEJRNWKEJRNWKEJRNWKEJRNWfghKEJRNWKEjjjjjjjjjjjjJjjjRNKWEJRNWfghKEJRNWKEJRNWKEJRNJKFNWEKJFNKJNFSKJDFNSKDJFNSKDJFNSKDJFNSKDJFNKWJENFRKJNSDFKJNWEFRKJNSDFKJNSDFKJNSDFsdfKsdfJsdfNSDFKJSNDFKJSNDFKJNSDFKJNSDFKJNgfhSDFJKNSDFKJNSDFJKNKSJDFNKSJDFNKSJDFNSKJDFNSKDJFNSKDJFNSKDJFNAJKLKLJADFGHKLSDFJGHJKLSDFGASLKDJASKLDJKLJLGHKLGFHDsdfFJGKLFSDJGLDFKGJDFKLGJLDFKGfghgfhjjJffddDllLFKGJAKPJDGHALJKGHKJHWERJKHWEJKRHWKEJRHNJWEKJNRKJWENRJKWENRKWJERNKWEJRNKWEJRNWKEJRNWKEJRNWKEJRNWKEJRNWKEJRNWKEJRsdfNWKEJRNWKEJRNKWEJRNWKEJRNWKEJRNWKEJRNJKFNWEKJFNKJNFSKJDFNSKDJFNSKDJFNSKDJFNSKDJFNKWJENFRKJNSDFKJNWEFRKJNSDFKJNSDFKJNSDFKJNSDFKJSNDFKJSNDFKJNSDgfhFKJNSDFKJNSDFJKNSDFKJNSDFJKNKSJDFNKSJDFNKSJDFNSKJDFNSKDJFNSKDJFNSKDJFNAJKLKLJADFGHKLSDFJGHJKLSDFGdfgdfgdfgdfgdfgdfgdfgdfgdfgfdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfg" },
                B = new List<string>() { "ASLKDJASKLDJKLJLGHKLGFHDFJGKLFSDJGLDFKGJDFKLGJLDFKGJDLFKGJAKPJDGHALJKGHKJHWERJKHWEJKRHWKEJRHNJWEKJNRKJWENRJKWENRKWJERNKWEJRNKWEJRNWKEJRNWKEJRNWKEJRNWKEJRNWKEJRNWKEJRNWKEJRNWKEJRNKWEJRNWKEJRNWKEJRNWKEJRNJKFNWEKJFNKJNFSKJDFNSKDJFNSKDJFNSKDJFNSKDJFNKWJENFRKJNSDFKJNWEFRKJNSDFKJNSDFKJNSDFKJNSDFKJSNDFKJSNDFKJNSDFKJNSDFKJNSDFJKNSDFKJNSDFJKNKSJDFNKSJDFNKSJDFNSKJDFNSKDJFNSKDJFNSKDJFNAJKLKLJADFGHKLSDFJGHJKLSDFGASLKDJASKLDJKLJLGHKLGFHDFJGKLFSDJGLDFKGJDFKLGJLDFKGJDLFKGJAKPJDGHALJKGHKJHWERJKHWEJKRHWKEJRHNJWEKJNRKJWENRJKWENRKWJERNKWEJRNKWEJRNWKEJRNWKEJRNWKEJRNWKEJRNWKEJRNWKEJRNWKEJRNWKEJRNKWEJRNWKEJRNWKEJRNWKEJRNJKFNWEKJFNKJNFSKJDFNSKDJFNSKDJFNSKDJFNSKDJFNKWJENFRKJNSDFKJNWEFRKJNSDFKJNSDFKJNSDFKJNSDFKJSNDFKJSNDFKJNSDFKJNSDFKJNSDFJKNSDFKJNSDFJKNKSJDFNKSJDFNKSJDFNSKJDFNSKDJFNSKDJFNSKDJFNAJKLKLJADFGHKLSDFJGHJKLSDFG" }
            });

            Answers.Add(new List<string>()
            {
                "YES"
            });

            Samples.Add(new AbbreviateSample()
            {
                T = 1,
                A = new List<string>() { "aaAaabbBbbccCccddDddesdfeEee" },
                B = new List<string>() { "ABCDE" }
            });

            Answers.Add(new List<string>()
            {
                "YES"
            });

            Samples.Add(new AbbreviateSample()
            {
                T = 1,
                A = new List<string>() { "aqwebcdabcdaaAaabbBbbccCcdddeEee" },
                B = new List<string>() { "ABCDE" }
            });

            Answers.Add(new List<string>()
            {
                "YES"
            });

            Samples.Add(new AbbreviateSample()
            {
                T = 10,
                A = new List<string>() {
                    "hHhAhhcahhacaccacccahhchhcHcahaahhchhhchaachcaCchhchcaccccchhhcaahhhhcaacchccCaahhaahachhacaahhaachhhaaaCalhhchaccaAahHcchcazhachhhaaahaahhaacchAahccacahahhcHhccahaachAchahacaahcahacaahcahacaHhccccaahaahacaachcchhahhacchahhhaahcacacachhahchcaAhhcaahchHhhaacHcacahaccccaaahacCHhChchhhahhchcahaaCccccahhcaachhhacaaahcaaaccccaacaaHachaahcchaahhchhhcahahahhcaachhchacahhahahahAahaAcchahaahcaaaaahhChacahcacachacahcchHcaahchhcahaachnachhhhcachchahhhacHhCcaHhhhcaCccccaaahcahacahchahcaachcchaachahhhhhhhhcahhacacCcchahccaaaaaHhhccaAaaaCchahhccaahhacaccchhcahhcahaahhgacahcahhchcaaAccchahhhaahhccaaHcchaccacahHahChachhcaaacAhacacaacacchhchchacchchcacchachacaahachccchhhaccahcacchaccaahaaaccccccaaaaaaaHhcahcchmcHchcchaaahaccchaaachchHahcaccaaccahcacacahAhaacaacaccaccaaacahhhcacAhaCchcaacCcccachhchchcchhchahchchahchchhchcacaachahhccacachaAhaaachchhchchchhaachahaahahachhaaaccacahhcacchhhaaachaaacAahhcachchachhhcacchacaaChCahhhccahChaachhcahacchanaaacchhhccacacchcahccchAcahacaaachhacchachccaaHacaacAhahcCh",
                    "XbxxobxBobbbxooXobXxxBOXoOboxxbobXOoBbxbXooXBboxooOxxXbboxoOxlobbObbXoXXbbXobbbXoxbxXBxoobooxbxoxoxOxxOxbxbxXobbbbBbxoxoooxooobXxbooBbOXxXxbxqobbbboXxoXXbbbxObXXxobOXXOxoOoxoXOXBxOxBoxbobxoBxbobobXooOxxOBXbxxXbooxbxooOxoxoobxxBOxxbbbxBxzXxbBxOobBObooofbbBXXOxxoxxbXBbOboxxooBbxOoboXoooXBbBbooOoBbbObxobxbBBoOxoxobBoOXXobObxobxOObobbbxxoboxoXxbXoxxxxbbobbXoXooBXXxboxbobxxxXboxOoOoxBoboOXboBoobXobxXdxObbbBxbxBbOOXbxooXboxboonxxxXOBbbXXoobooxbbxboxoOxBBbxBOxoobXbbxxbXXObxBbxBXBxoxOxoBbxBobOXbboxooBxbooXbXbooBbbxXboxXbxXoxbboxOXOooXbobooXXoxobbxoOxOoBbxxoBboboxoOBBxoboBoOboxbbxxbbbObXbboXbObOjXOXBxbxXobbbboBxBoOooxbxxOooxxbxxobbobxbbXoOobbBXoObxobXxoobxBxBbxoobXxoxObboxobobooxOoooBBbbbxxXoxbXxoXooxOBxboobxooxXOxobXoXmObxxXObooXXXboOXxbXxObxxbbObObxbxxbxxBXxBxoxOooaxooxXBXoXOxoOXxbBoBXxXooboXboOooxoxOxXxbxoboOObbBoXxbboxxooBBbooxXBbBoxBOobbboobobooxoxOxoXOXXboxXOboBxoboOooxbxBxobooXOoxOOObbxbobxxoxbOBoBxboxoobbbxoooxBxoobBbobBbooOBbxoboooookxXoobbbbBbOoxOBOobXObXBxoXoboxobbXBXBBoxBxoxooOxobxo",
                    "laalsAsaasLbbabLslalBbssaAsAlSLsbBllsSalblsssbsaaaAsabBaaAaalsssasssssLbasbbllbbLSsslbabAbSlllsbsbbalbBaSaaalbslaabAAaaabsabSlsassSshBBllbAAllsSbaLblabsaLBasBsAlLaabBbAllbaslsllsaAaAabbSallbLalsslbbblbasBAsbaBLalbBssbbAlbbbsSlsllbaLBLaaLblalBSbsBbSsbbaaSlllsblbsSaaBbassslaalblBbslLlaASASbbabbLlbalSabbBbLsbaabbalsAAbSbBbABbabbabaallBsasllbsbbsslSsbBlBlbabaalblaLsllbasasalabllSsbslLbsllbLsBlaSbssSAbsSasbsSalsabbllbbaBSBlabsBlAsbaSLbSllbsAblllSLaaAlBssSsBSLslAAlsbslbalsbSbsbalbsBabSbbsssaaabassalslllbsSLSsaLlbbBslSlSbbslsbslSLbbSbAaaaalLlSlAslsbmslbbalblLabSslassBabllSAsbbsvLllSalalbsaaaLAaSSbLbblaaSbLaalABlabsAsBsalssbBLlsLssaabsslabpSbsBaBbbSBlsaaabbblslBAblsLaASlaAlbaaSssbblalAaasbaalbLlaabbAaaaaAalsabbsllaaAsallsasBbAaslbbsbllbbllbslaBASbbSblaAbbsbbssAaBbsasLllalBlslssasbssBALAasbbsbSfasabbllbAslbalbaSSlslbbSbsaBsAalablAbbaBBsbsSbdaAsBblsblbABbLAssAbalsbssSssbBBssAsABLssblsLbllSblasllLbBsassllBbBbsbBsbllsBBsAbbLLlAslBlsAAASlaalabasaLslasBLlsslsaaslsbblbAsalSlllsLSAaLlalAalsBsaslaaaalb",
                    "EerRrrreeReeeErEEEEeRErerrrreeeerreerRrrEeeeReerrrrErereeRRRrREReERrEerREeEsrrrREeErererrRreERrrErreeeEEeReRrerrrERrreeerrrreEerrerreRerrRerRererrereeeerrrrReRRRrRreerrererReRererRrRRerRRreRRrRrrrEeRrrEEeeEeerErrereErEerreeeREererrRRERrerrrrreEerRrrrEeRERreereeereErerrrrrererErerErREEereReeeEeerRrerrrrrrreerErreEReeererrreeeeeeeEereErreerEerrerERrrereeEeREeRerErErreRrrrRrreEERrrRErrRreeEeEeeeerreRerRreEReeEeRERRreReEeerEErrrERreeErerEeERrRrrReerrreeeEerrerrrerrrrreeerreEerReRRErEerreerReeeeerrRrrrrrreerRreeEEereeeeErrrrrererErREeRrrReerrErEeEeerRreeeeeRrERerRrrererreReerereeerreRreeereERrrErreeeeerrrrrEReErrRerrErereEeerrrerrEerreEeererRererReeeErEereeRrrerreEeRrrRrrErrEereRreeereErRerereeeeRrRrreeerrrereereeERrRrrrRRrrRReerEEEReerRrererREereerRrrerrEeeererRrERreEreRrrRreerReRerreereRRreeERrEeEreErEreERrerEerrereEeerereerrEREeeEerserrrrreeRrRerrrERreEersEReREEerRrerrrReErrRerreReerererrrRErrsEErrErEEerrEREreeErrrEereRrrErRereeerREeEeEeerrrererrrRrRrEreERrrrerEerrrRRrerreereeRereerreeee",
                    "YYRlYyrYYrLYYyrllRLzrLLRrRylyylrRRyYYLLYllyyLllyLrLrYlrryRsYYYYYlyRrrrRlRRrLlyrlLLLRYlrrrrrlrrylYRRLlllRyyYRlyyLYryrRyyryLiLylyryyrlRLLLrlLRLYrylYrLylyyRrLyyylYlLrrylYyrLLrwyRRylLRrllYRrrrYlLyyRLrLRlLLlLYLYYYlRyyYYRyrllrrlllyrLrlllLrylRRrLLrRRlyrrRRYyYlllyLrRLlRlrRrYyLLjyRLlLYyryYyrylrlrLyylRyryrLLyLYLyLYrYlLYrRRrlYlyrryRYllLLLyRrRRylLyYlYyRLRLlyRRlrrRRrlRlRlryRRyRYRrryYYYlYYrLlllYrlRRlryyllYyRlYyRLrRrLYYyLrrYllYRyLlylLlrlRlLlryLryRlYylRLYlLYrlyRrrrllYlYLLYRRrRRLLYLlYllYlyrLRylRYlLyLRLrRYLyRlmLLlYlLrRlLylLlrLYyyYYYrRlrrylyRylryrLryrlYrlLrYlyryryRLyLlYRlYyrYRyylryYRryyylRylryRRyllrlllYrYRrrlYyRRyrYlYpylRrRYRLyRRRrrrYRLylrlRyyRlylYLYYRYlryLlRLYyRLLYyllRlRlRlyylRYRlLlYlyRlYrYRYLyYRyRLrRYrYrLYyYYlRryrrlylYyrlRrryYllnRLyYlYryrRLlYyLryylYyRRYLrLlyrYlRLlRLryrylLLRLrlYYyLrLyrYLyLrYlYYrYYlrLrllYlllYYlrlRyYRyLRRRrLrlLyRLRRLrLRrLyLRYRLYyRLLyRRlRLllyrYlRllrLlLyLrLYlryYYlLyLlRRRrfyLLrlRlyYRLrYyYyYylyyyryllYLRRrLrylYlyRyYLlnrYrLyRRRyRYYYrYrRlyLryyrrrlygrrRRyRYRyrRRllRlrYlRylryLryylrryYlYlyrllLllyYYY",
                    "vvvkeevekVVvvkkKeeVvKvkevkvvkevevkeveekkkvekVkeEevVvkkKevekkkkekkekvevkevekkevEvEkEvvKkkveveKkKkkEekevkveKvkKkVeeEeeeekkekekvvkkvkeKvkKvkeKkKKEvvVekeKeKvvkeKvveeEEvkeveekVvekkkkevkkkkekVvvvkkEekkvkekVKvekVevvKkvvEveeekkEvevkekeveVkKevkVkeEvkeeEkkvekeevVvvvvkkvveKeevkkevvVekvKEEVvkvVkeVkEkkvvEekVkekevvVEEvvkKkkevEeVKvEekkVkVvkkevkvVeEeeeEvvkkkeVkeeVekEkeevkvVkKeevkKvkekvVevvvVkvKKKevekEvVekvEVeKkKKkvvevekvevkKvvvVvEEkEeveekKKVEKkkVKvKevkveVvVveeKVekEkkevvveveKeevkVvvEVeEkEkKkkeeeveeekekKeVvekevEKvkkkkVkEkekeEEekvkVVkekvKKkeeEvkeVkkekkvvKvKvEEVvvvVekkeeekEvvKvvvvVVEeeKVKvVekeekvvekvvekEeKekeEeVVEeeKEeEEvvveEevVEVkEvkeEkeveeeeevkkeVkVKvvvekeveKekeekvevkveEkKkkevEkKkvveKkkkvvEEvKKeVekVVkveeekEvkkvKvkvveEvKkvEvvveKKekevEeekKekkkkvKkkEvKkeEkvKvkevekVKVKvveKvkvvkkVvkvKEKvvvvkeekevkEVvKeKkkevVkkEEkkvkveVVvevevekkkeEevEkeVkeKkVekvEevkeeEvVKVeeVkVekkvevekEeKevvvkeevvvVevKvVevVvVkkevEeVkEvEvevEeevKkkevekkvEkvkvKkeveveekvveKvKeeeeekEkEkKeKkkkvkEkekvEekeekkvvvekveekeeveKekeVevEVekkKKKvkekkkEvkeekke",
                    "RreeerrEeRReReErrrReerresrERrrrReTrreEerRRerRrereeSeeererEeEeeErEREErrErrreeeerRerrrrRerEREeererSeRrReRerrrerrErEReeerrrrrReEreerReerRerREERreeeeRreReeeeeEErRRrrererreRreReeereRrerreRrREEeReEErrrreeErEeReRrrerrrEreereereeRrEeRrreREeeerEReREerrrrreerErEErrrrRErrrRrreeReReereERerereReRreEeeeeEEeerrrerRrrrrRerreeeEEereeereeEeeEseeReEreRRERrrrereerrererrEReerrrrrreeeRrreeeeeRRrsrrREererERRreereeRereEeRrRrRereEeeeRreEeerrRrereRerrrerererErRererrreeEeRRrErErrErrerrrreerrrreReeererersreReerEreRerReRRreEeeReereeEerrEEErrrEererreeerreeerrrrrEeeEEerrrReReeerreeREeEeREeReeeeREeRerERerreRereeslrrreeerERerErrRreRRrreEeererrrRRRreErrRREreeEeereeerrreeerrEerrrRRrerrerRReErRRrreEeeRereeEeERreEeEerREErReReRerrrreeERErereRreeReeeeeeErrreeerEeerEREeeReereerrrrerrErerrErerRrrErerrReEEerReeERRRrErereeeerERerRrRErSeEeeeeRrEereeeErrrREerERerReeeeReerRrreeEEeEerrereeeererEEERseeererRrrRerreersereeeRrreeEerrrrrreRERrErerrreRrrererRerererRreErEErrRrREreRrRrerReEeRrErrerererrreerreEReeRererrreReEEererREeEvRRrrer",
                    "ANzaNanaanAZnnaazzzNAznnZaaZzzaZzzznaaaaZAANnaaanZnzazaAANanZaznazznzaAaNznazzanaZznzANzznzaaZzAnanNanzzAazzZZananazAznaznNznaAAaZnnanzazANAANAnnnzazaaaanzaznAaaNZnNAnnanazaZzNzazanZnazaAzanazzaNznNzzzaaanZaAnNAanzznNaNznanAnananNnaazznznnzNznnNzzanzAaNzzzZzAnnznaanzZznzNZzZzznnnaazzzanaazzazznnanANnznzAZzNZnNnanzazNaZZzzazAnNzAzAZAazanzzZzaznnZzaaazzznnaanaazaAnzzzZaaazzzzNaaNazzaaANznazAannzAaZZaznnzznnAzaaaanaaAznazZAnzzaAzaZzzZzznzazAznnaznznnaNAazZzzazNazanzaanZaZznnznzaNzannnZZNnaznzaNaAZznazAzAzNnnanznannaznAznnnnazzNnaazAanzZnaAnnaAzaanZnZNNzannanznazAnzNanaZznAAnnnNzaznAnZZnznaanzzaNzzAZzaNzNzaZanaNzNnnnAnaaZnaaznanZnzaannanzAzazazaNannaaznNnNnzaazazAzAnAzzaNaaNnanzaaZANaaZnaAzazaZZZAznAaaZnaAnnAanaAAnznNNzNnanZzzZzzNzaZaaznnznzNnaNZannNzAnnnznAazaaaanZzzananznzzZznNNzzznnznannZzznzzaZazaNnnnZzanznazzazzanzazzZannzAzazAZnnzNZannzZaNznAZanaaanAnNzzznzZaanANZananzzZaNzzaZnnzazZanzznAaaAZZaznANNzanaaanNzAnaanaAzzZnNannznaNznANzznzZanaNNaZnzaznzZaanzznnnAANzzZananzNZnaaZaANZzNAAaz",
                    "evivevvVvevvEeeevVevevVvevEVevEeeEVVveevEvevevEeEeEEeeVevvvVVvevveevevEeVeVvvveeeeeivVveEevvEeveevvevvvVeevVeeeeveebveeVvvvvvEVEvEvvvvEEVVeeeVvEVEvevvevEvveVeEeeeVveVEVvvvvEeeVVvVeEEVvEEeeeveveeevVVeeeevVveeVvevVVevevvEEveVEEVVeeeEeveevevEvvvvevveeeeVEvevvEVvvvVevevvvveVeevVveevVeEevEEeeevVeieeeVvvvevvVeevvveeeevvVevEeevvvevveveevvveeeveeevVivevvevVvVeeevvEveevEEVeeVVEeeeevvveeEeveevvVeeEeevvvveeVvveveeeEveeeEeveeveVeVEveevvvVeevvveeEevVveeeVEeevEveevvVeveeeeVVVVeVEvvEVveveEvVeeeeEeeevvVEvveevvEeevevvVEeeeEvvvevvVvEVEvvvvvVvevEvVvVeevVevvVvEvveeeeeeEeveeVvEvVVvveveEvvVveeevvvViEeEEveeevvVevEveVVVeEeevVeveeEeeeeeveEvvVEeeEeveEvvvvveeVveVeVveeeVveEveeVvEVeEEeveeeVvvveEeveveeeVevevvveveVEEeveveevevveeevVeeeveveeveeveevvvEeeEvVeveevVEVEvvVVeeveVevViEEvVevevvVVEVVvvVeiEvVeevevvvEevvEvvvvevVveeVvvEevEeEEvEeeeeevveveevveveeeeVVeeveevvvveeVEEEveeeveeeEveeVVeeeVvEvvevveevvveVveeievivvVvvevevveeEeVEEeveveeVEEveviEveeivVvvVIEeEEvEveevEEveVvvEEveVeVEVvveveeVvVeEveeVVvveveeveVeveevvevEeVeeveVeEeVve",
                    "cCccCoccocOOCCOccoccoCooCocoOoCcoCoooooOcococccOoocCoccOcoCcoooocCoooocCwcooowcocoocOococoocooOooCCooccooCCocooccoCoococccCccocoOoCcOCocccocOoOooOOooooccOcococcOOooCccooCoccOccoCcoCccOcccOoCCococCooOCoocccocoOocoOCCcoccOcOcccoOooooOOOoOcCcocCoCoOCOOcOcOOocooooocoCccocooocoooocccccooccccCCcoocococCcccCOcccccOoooOoooCcocccooocoCccOCCCccooccOwcCoccCcCcccocooOocCocccoOccocooccOocccooocooccOcccocoocoOOCOocOoococooOoOcocoocOcCcococcocCcoCoCOoOcoOOccoCcOoococoCooocccCooccCCcccCOooocoCOoOCcCccccocwcoCCOOcOoOccccCcocoCCococcCooOCcocccocOcocoocooooCoccccooOccCocoOOocococooOcccCocoOoccoCoocOccOoOOooooooocCoocCCcccococoooocCcoOooooOCcOccCooooOoocccccocoOocoCccCCcwOoOcocoocoOocccoOoCccocoocccccccooowccccOcCCoocooocOooococOOoooccoOwooOCccccoooocCooooooooooCwcooCcccoOcCoooOoOcwOoCoCcCocwoOOCcoocOOcCooocOoOooOoOccOcccocCoOcOcocoococcOoooccccccCoCooCcoooocCccOCccCooCcoOCcocoocOcoocoooOocCcCcoocoCOoOoocooCococoOccCoCoooocOoooOcoooCccocoocococOcOCccccoocccccccCooOoowoOcooOcCCOoCccCocooccoccoCCoccocOcccCo"

                },
                B = new List<string>() {
                    "HAHHCHAACCCAHCHHAHHAHCACCHCCHHCAAHHCACCCAHHHACAAHHHHCHHCAHHAHHAAAHAACAAHAHHCAHAHACHACHCHACACHAAHHAAAHCAHHACACAACHHHCHAHCAHCHHHAHAHACCAAAHCHHCHHCCAACCCCAACHACAACAAHACHCHAHHACCHCAHHHAAACHACAACHCACACAHHCCHAHACCCACCAACHCHHHCCCCCHCCAHHCAAHHAHHHHHHHAACCCCAHCCAAAAAHHHAAAACCAHHCAHACACCHHCHAHAHHCHAACHHHHHCCHCCAHAHCHCAAACCACCCCHACCACHHACHHACACHACCAACCCCAAAAHHAHCHHHCCAHCCHACHHAHCCACACCHAHAAACACCCCAHCCAHACCCCCCHCCHHCHHHHCHCHCAHHHACHAHAACCCAAAACHAACAAAHHAAHAAAHACHHCACHCCHCHAACHACACHHCCCCCAHCACHAAAHCHCAHACAAC",
                    "XBOBBOBOXOXBOXOOBOXOXOBBXOXBXOXXBBOBXOXXXOBBXBOOOXXOXBBBXOXOOOXXOBBOXXOBBXXXOXXXOXXOOXOXBOBBBXBBXOOXOBXXOOOOBXBOXXBXBXXXBXOBBOBBXXOXXBOBBXOXXBBOOOBBBOXBBBOXXBXXOBOBXOOOXXXXXXOBXXBOXXOOOOBOOOXBBOOBXOXXOBBBBOOXXOOXXXOBBXXOXBBOXOXBBBOXOBXXXBXXOBBXBOOBBBOXBBBOXBXBOBBXXXOXBOOXOOXBOXXOOOOBBBBOOBBOBOOBOBXBBOXBOBOXOXBXOBBOBBOXBOOXXBBBBBXOBXOBXXXBBBXOOBOOOXOOBBBXXOXXOXOBOXOBXXOXOOXXXOXXOBOOXBBXBOXBXXOXOXBOBXXOOXOOOXXBOOBBXXXBBOXBBXBOBBOOBOOXOXXBXOBOOOXBOXOOXOOOBBOBBOOOBBBBBOOBOXBBBOBOBXOXBXOBXBXBXBBBXOOO",
                    "ALASALABLABBASASLSBLSSLSSASBBAASSSLBBLLSSBAASLBBASBSLAAASSSSSBBLAALSALLBASBALLBBABASLLSAAAASLBBABABLABBALBSALBLLLBSSBSSAASSBBALALBBBSLLLASASLASABBBLASAASBBABAAALBLLBSSBBLBLBLLLAALLLSSSLLSLLBSSSABSSSSSBABSBLASBASSLBSLSASLAALBSBSLAASBSSBBSBBSSLLBSLSSLLBBLSLSSLBBSAALSASLSLBSABALSASLLLSLLAASSLBASBLAAABBABSBLLASASSBABBSBBBBLBABSLAASAASSSBLALAAASBLLBBAAABBLAABASSBBBLBLABASBSLAABBBSAABBASLLBSSSBBALASBSSBBASSLSBABABBBSSAABBLBABLAAABSBBSAABLSSSLSLLBALBBBBBSBBSABLLABLSAAASLABAALSABLSLBLBASASLLSALAABAAAALB",
                    "ERREREEEEEEREERREEERERRERRRERERRRRREREEREERREEERRREERERERRERREEERRRERRRRERRERRRRREERRRRRRRRRRRRERRRRRRERRRREERREEEEEREERERREEERRRERERERRERERRERRREEERREEEEREEEERRRREERERREREEEEEEEREREEEEREEREREEERRRREERREREEEEERREREERERRREEEEERREREEREEERRRRERERRRRREERERRRERERRRRERRREEREERRERERRREREERREERERRRREREREERRERERERERERRREEEEERREREERERRERREREERRREEERRREREREERERREEEERRRRRRREEERERREEEREERREREERRRRRRERRERREEREEREEERRREREEREREREEERERRERERERSERREERREREERRERREREREEREREEEREEREERERREREEEREEEERRERRREERRERERRREEEREE",
                    "YYRYYYLYYRLLLRRLRRYYLLYLLLLYYRYYYYYLRRRRLLLLRYRYRRLLRYRYLYRLLLRRLLLLRLYYLRLYLYLLRRLRLYRYLRLLRLLLLYLYYYRYYRLLRRLLRRLRRYYLLRLRLRYLLRLLYRYLRLLLYLLYYLYRRYRYLLLRRRLYYRLRLYRRRRRRYRRRYRRYYYYYLYRRRYRYYRLRLYYLRYYRLLLRLLRYYRLYLYRRRYYLLYRRRRLLYLLYYYLRRYLLRLRYLRLLYLRLLLYYYYRRRLYLLYRLLYRYYRYRRLRRLYYRYYRRRYYLRRYRLRRRYRLRRYLYYRYLRLRLYRLLYRRRRYRLLYLRYYRYLYRRLRRYYLYYYLRYYRYYRLYYRRLYLYRRYLLYRLRLLLRLYYLRLYLLYYYYYLLYYYRYRLRRRLLRLRRLLRLLRYRLYRLLRRRLYRYRLLLLYYYLLLRRRLLRYRLYYYYLRRLYRYLLYLRRRRYYYYRRLRRRRYRRRRRYRLYYLYYY",
                    "VVVKEEVEKVVKKEEVVKEVKVVKEVEVKEVEEKKKVEKKEEVVKKEVEKKKKEKKEKVEVKEVEKKEVVKVVKKVEVEKKKEKEVKVEVKKEEEEEEKKEKEKVVKKVKEVKVKEKVVEKEEVVKEVVEEVKEVEEKVEKKKKEVKKKKEKVVVKKEKKVKEKVEKEVVKVVVEEEKKVEVKEKEVEKEVKKEVKEEKKVEKEEVVVVVKKVVEEEVKKEVVEKVVKVKEKKKVVEKKEKEVVVVKKKEVEVEKKKVKKEVKVEEEEVVKKKEKEEEKKEEVKVKEEVKVKEKVEVVVKVEVEKVEKVEKKVVEVEKVEVKVVVVKEVEEKKKVEVKVEVVEEEKKKEVVVEVEEEVKVVEKKKKEEEVEEEKEKEVEKEVVKKKKKKEKEEKVKKEKVKEEVKEKKEKKVVVVVVVEKKEEEKVVVVVVEEVEKEEKVVEKVVEKEEKEEEEEVVVEEVKVKEKEVEEEEEVKKEKVVVEKEVEEKEEKVEVKVEKKKEVKKVVEKKKVVVEEKKVEEEKVKKVVKVVEVKVVVVEEKEVEEKEKKKKVKKVKEKVVKEVEKVVEVKVVKKVKVVVVVKEEKEVKVEKKEVKKKKVKVEVEVEVEKKKEEVKEKEKEKVEVKEEVEEKEKKVEVEKEEVVVKEEVVVEVVEVVKKEVEKVVEVEEVKKEVEKKVKVKVKEVEVEEKVVEVEEEEEKKKEKKKVKKEKVEKEEKKVVVEKVEEKEEVEEKEEVEKKVKEKKKVKEEKKE",
                    "REEERRRERERERRRRETRERRRRRERESEEEEEEEREREEEERRRRRERERSRRRERRREEREERRREERREEREREEEERRRRRRREREEEREERRRREERRERRREEERRERREEEEERRRRERRRREEERRRREEEEEEERRRRREEEEEEEEEEESRERRERRRRRERRRERRRRRREREREEREERRRREREEREERRRERERRREERREERERRERRRESERERRRERREREEEEEEREERERREEREEEERREREEEREEERERRERRERREEEEERERRRRREERRRREERRRREEERERREEERRRRRERRREREEEREEEREERREREREEREEREEEERREEREEEREREEREERERRERRERRREEEERREERRREREERERRRERSEEEEREERERREERERERREEEREEREEERSERRREREEERREERRRRERERREERERERREEREERRRERRREEREEEREEREERERREEEEREEERRR",
                    "ZAANAANNNAAZZZZNNAAZZAZZZNAAAANAAANNZAZAANAZNAZZNZAAZNAZZANAZNZZZNZAAZNANANZZAZZANANAZZNAZNZNAANNANZAZNNNZAZAAAANZAZNAANNNANAZAZZAZANNAZAZANAZZAZNZZZAAANANANZZNAZNANNANANNAAZZNZNNZZNNZZANZAZZZZNNZNAANZZNZZZZNNNAAZZZANAAZZAZZNNANNZNZZNNANZAZAZZAZNZZAZANZZZAZNNZAAAZZZNNAANAAZANZZZAAAZZZZAAAZZAAZNAZANNZAAZNNZZNNZAAAANAAZNAZNZZAZAZZZZNZAZZNNAZNZNNAAZZZAZAZANZAANAZNNZNZAZANNNNAZNZAAZNAZZZNNANZNANNAZNZNNNNAZZNAAZANZNANNAZAANNZANNANZNAZNZANAZNNNNZAZNNNZNAANZZAZZZAZZAANAZNNNNAANAAZNANNZAANNANZZAZAZAANNAAZNNNZAAZAZZNZZAAANANZAAAANAZAZAZNAANANNANANZNZNANZZZZZAAAZNNZNZNAANNZNNNZNAZAAAANZZANANZNZZZNZZZNNZNANNZZNZZAAZANNNZANZNAZZAZZANZAZZANNZZAZNNZANNZAZNANAAANNZZZNZAANANANZZAZZANNZAZANZZNAAAZNZANAAANZNAANAZZNANNZNAZNZZNZANAANZAZNZAANZZNNNZZANANZNAAAZAZ",
                    "VEVVEEVEVVEVEEVVVEVEEEEEEEVVVEVEVEVVEEVEEEVVVEEVEEVVVEVEEVEEVVVVEVEVEVVEEVEEEEVEVVEEEVVVEEEVEEEVVEEVVVVVVEVVVVVEEVEEVVEEEVEVVVVEVEVEVEVVVEVVEEVVVEEEEEEVVIEEVVVVEEVVVEVEVVVEVVVEVIVEVVVVEEEEEVVVEVEEEVEVEEVEVVVEVVVEEEEEEVEVEVEVVEVVVVVEVEVEVEEEEVVEVEEEEVVEEEEEVEVEVEVVVEEVVEVVVVEEEEEVEVVVVVEEEVEEVEEEEVEEVVVEEEVEEVEEEEEEEVVVEVVEEVEVEEEVVVVEVEEVEVEVVVEEVEVVEVVVEVEEEEVVEVEVVEEVVVEEVVVVEVVVVEVVVVVEVEVVVVEVEEEEEEVVVEVVEEVVVEEVEEEEEEEEVVEEVEVEVVEVEIVVEEVEEVVEEVEEEIVVVVIEEEEEVEEEVEEEVVEVVEVVEVVEVEVEVEVVVEVE",
                    "CCCOOCCOCOCOCOOCCCOOCOCOCOCOCCCOOCOOCOOWCOOOOOOOOOOCCOOCCCCCOCOCOOCOCOOOOOOOOOOOCCOCOOCCCCCOCOOCCCCOCOCCOOOOCCCOCOOCOOOOOOOCCCCOOCOOCOOOOOOCCOOOOCCCCCCCCOCCCCOOOOOCOCOCCOCCCCOCOCCCCCCCOOCCOCCOCCOOOCOOOOCOOOOOOCCOOCCOOCOCCCCOOOOOOOCOCOCCCCCCOOOCCOOCCCCCCOOCOOCCCCCOOCCCOCCCOCOOCOCCOCCOOOOOOOOOCCOOOCCOCOOOOOOOOCCCCOCOOCCOOOCOCCOOOOCOOOCCCWOOOCCOOCCOCCOCCOOCOCCCOCOOOOOCOOOCCCCCOCOCOOCOOWOOCCCOOOCCOOOCCOOCOOOOOOCOCCCOOCCCOOCOCOCOCCCCOCCCCOCCOOOOOCCOCCOOOOCOOCCCOOOCOOOOCCCOCOCCCOCCOOOCCOOCCCOOCCOCOOCC"
                }
            });
            Answers.Add(new List<string>()
            {
                "YES","NO","YES","YES","YES","NO","NO","NO","NO","YES"
            });
            */


        }

        public override bool CheckAnswer(int SampleID, List<string> Answer)
        {
            //return base.CheckAnswer(SampleID, Answer);
            try
            {

                for (int i = 0; i < Answers[SampleID].Count; i++)
                {
                    if (Answers[SampleID][i] != Answer[i])
                    {
                        return false;
                    }
                }
            }
            catch (Exception)

            {
                return false;
            }

            return true;
        }


        static byte[] NumStringToByteArray(String s)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            Byte ZeroCode = (Byte)'0';
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] -= ZeroCode;
            }
            return bytes;
        }


        public static List<String> Solution1(AbbreviateSample sample)
        {
            Int32 T = sample.T;
            List<String> res = new List<string>();
            for (int i = 0; i < T; i++)
            {
                res.Add(abbreviation(sample.A[i], sample.B[i]));
            }
            return res;
        }

        static string abbreviation(string a, string b)
        {
            String aU = a.ToUpper();
            Int32 BPtr = 0;
            for (int j = 0; j < a.Length; j++)
            {
                if (aU[j] == b[BPtr])
                {
                    BPtr++;
                    if (BPtr == b.Length)
                    {
                        bool flag = true;
                        for (int k = j + 1; k < a.Length; k++)
                        {
                            if (Char.IsUpper(a[k]))
                            {
                                flag = false;
                                return ("NO");
                            }
                        }
                        if (flag)
                        {
                            return ("YES");
                        }
                    }
                }
                else
                {
                    if (Char.IsUpper(a[j]))
                    {
                        return "NO";
                    }
                }
            }
            if (BPtr < b.Length)
            {
                return ("NO");
            }
            else return "YES";
        }

        static string abbreviation2(string a, string b)
        {
            String aU = a.ToUpper();
            Int32 BPtr = 0;
            Int32 APtr = -1;
            Int32 APtrPrev = -1;
            while (true)
            {
                APtr++;
                if (APtr == a.Length)
                {
                    return "NO";
                }
                if (aU[APtr] == b[BPtr])
                {
                    BPtr++;
                    APtrPrev = APtr;
                    if (BPtr == b.Length)
                    {
                        bool flag = true;
                        for (int k = APtr + 1; k < a.Length; k++)
                        {
                            if (Char.IsUpper(a[k]))
                            {
                                flag = false;
                                //return ("NO");
                                if (Char.IsUpper(a[APtr]))
                                {
                                    return "NO";
                                }
                                else
                                {
                                    APtr = APtrPrev;
                                    BPtr--;

                                }

                            }
                        }
                        if (flag)
                        {
                            return ("YES");
                        }
                    }
                }
                else
                {
                    if (Char.IsUpper(a[APtr]))
                    {
                        //return "NO";
                        if (APtrPrev > -1 && Char.IsUpper(a[APtrPrev]))
                        {
                            return "NO";
                        }
                        BPtr--;
                        if (BPtr < 0)
                        {
                            return "NO";
                        }
                        APtr = APtrPrev;
                    }
                }
            }

            if (BPtr < b.Length)
            {
                return ("NO");
            }
            else return "YES";
        }

        static string abbreviation3(string a, string b)
        {
            if (a.Length == 0 || b.Length == 0)
            {
                return "NO";
            }

            if (a.Length < b.Length)
            {
                return "NO";
            }


            String aU = a.ToUpper();


            for (int i = 0; i < a.Length; i++)
            {
                if (aU[i] == b[0])
                {
                    if (b.Length == 1)
                    {
                        bool flag = true;
                        for (int k = i + 1; k < a.Length; k++)
                        {
                            if (Char.IsUpper(a[k]))
                            {
                                return ("NO");
                            }
                        }
                        if (flag)
                        {
                            return ("YES");
                        }
                    }

                    var ans = abbreviation3(a.Substring(i + 1), b.Substring(1));
                    if (ans == "YES")
                    {
                        return "YES";
                    }
                    else
                    {
                        if (Char.IsUpper(a[i]))
                        {
                            return "NO";
                        }

                    }
                }
                else
                {

                    if (Char.IsUpper(a[i]))
                    {
                        return "NO";
                    }
                }

            }
            return "NO";


        }

        public static List<String> Solution3(AbbreviateSample sample)
        {
            Int32 T = sample.T;
            List<String> res = new List<string>();
            for (int i = 0; i < T; i++)
            {
                res.Add(abbreviation3(sample.A[i], sample.B[i]));
            }
            return res;
        }



        public static List<String> Solution2(AbbreviateSample sample)
        {
            Int32 T = sample.T;
            List<String> res = new List<string>();
            for (int i = 0; i < T; i++)
            {
                res.Add(abbreviation2(sample.A[i], sample.B[i]));
            }
            return res;
        }

        public static List<String> Solution4(AbbreviateSample sample)
        {
            Int32 T = sample.T;
            List<String> res = new List<string>();
            for (int i = 0; i < T; i++)
            {
                res.Add(abbreviation4(sample.A[i], sample.B[i]));
            }
            return res;
        }

        static string abbreviation4(string a, string b)
        {

            Int32[,] d = new Int32[a.Length + 1, b.Length + 1];
            String aU = a.ToUpper();

            for (int j = 0; j <= b.Length; j++)
            {
                if (j == 0)
                {
                    d[0, j] = 1;
                }
                else
                {
                    d[0, j] = 0;
                }
            }

            int count = 0;

            for (int k = 1; k <= a.Length; k++)
            {
                int i = k - 1;
                //if (a[i] >= 65 && a[i] <= 90 || count == 1)
                if (Char.IsUpper(a[i]) || count == 1)
                {
                    count = 1;
                    d[k, 0] = 0;
                }
                else { d[k, 0] = 1; }
            }

            for (int k = 1; k <= a.Length; k++)
            {
                int i = k - 1;
                for (int l = 1; l <= b.Length; l++)
                {
                    int j = l - 1;
                    if (a[i] == b[j])
                    {
                        d[k, l] = d[k - 1, l - 1];
                        continue;
                    }
                    else
                    {
                        if (aU[i] == b[j])
                        {
                            d[k,l] = d[k - 1,l - 1] | d[k - 1,l];
                            continue;
                        }
                    }
                    if (a[i] >= 65 && a[i] <= 90)
                    {
                        d[k,l] = 0;
                        continue;
                    }
                    else
                    {
                        d[k,l] = d[k - 1,l];
                        continue;
                    }
                }
            }

            if (d[a.Length, b.Length] > 0) return "YES";
            else return "NO";



        }



    }
}
