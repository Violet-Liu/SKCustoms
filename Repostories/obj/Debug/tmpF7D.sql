create table `CaptureBackup` (`ID` bigint not null  auto_increment ,`ParkId` varchar(50) ,`CarNumber` varchar(200)  not null ,`CarColor` varchar(200) ,`CarType` varchar(200) ,`Channel` varchar(200) ,`ImageUrl` varchar(200) ,`Pass` int not null ,`Remark` text,`CreateTime` datetime not null ,`WithOut` int not null ,primary key ( `ID`) ) engine=InnoDb auto_increment=0
CREATE index  `IX_CarNumber` on `CaptureBackup` (`CarNumber` DESC) using HASH
INSERT INTO `__MigrationHistory`(
`MigrationId`, 
`ContextKey`, 
`Model`, 
`ProductVersion`) VALUES (
'201807200709419_AutomaticMigration', 
'Repostories.Migrations.Configuration', 
0x1F8B0800000000000400ED5D5B6FDCB8157E2FD0FF3098C745EA89374D2F81BD8BAC9D6C8D8D63C3E36CFB16C833F45888469A4A9AACDD627F591FFA93FA174A8914C5CBE14DA2E6E21D04083C24F51DF2F0F01CF29087FCDF7FFE7BF2FDE332197D45791167E9E9F8F8E8E57884D259368FD3C5E9785DDEFFE12FE3EFBFFBFDEF4EDECD978FA39F9B72AFAA72F8CBB4381D3F94E5EACD6452CC1ED0322A8E96F12CCF8AECBE3C9A65CB4934CF26DFBE7CF9D7C9F1F104618831C61A8D4E6ED669192F51FD03FF3CCBD2195A95EB28B9CCE62829683ACE99D6A8A38FD11215AB68864EC7376895156596C7A8188FDE267184AB3045C9FD7814A569564625AEE09B4F059A9679962EA62B9C1025B74F2B84CBDD47498168C5DFB4C55DDBF0F2DBAA0D93F6C3066AB6C6355A7A021EBFA24C99C89F7762ED98310DB3ED1D666FF954B5BA66DDE9F86D12E5CBF148A6F4E62CC9AB52A7E3739C1CA74775B91723F2EB05EB702C17D5BF17A3B37552AE73749AA2759947C98BD1F5FA2E89673FA1A7DBEC0B4A4FD37592F035C175C17942024EBACEB315CACBA71B744FEB77713E1E4DC4EF26F287EC33EE1B52F98BB4FCD31FC7A38F9878749720D6D15C43A75864D08F28457954A2F975549628C7FD74314735AB14EA12ADB36855B5FB622E91347F5573F3160B7AF3D539264E7E2B75B5D1CF3FAE9777286F90B078E3213A1E5D468F1F50BA281F4EC75818C6A3F7F1239A372914FA531AE3118D3F2AF3B53FE98BE21C4509D7EE57DF7A635CAD2ABE67216A6F2674898A225A20031DFC67003A3F7E6AA56F30221F5025A567B8FCE07C3B7BC063052583D3F95B94CE1394C36342FAF463F4355ED4A3171E8CE3D10D4AEAFCE2215E112B4014D86756E27D9E2D6FB2A4198B4DC6E769B6CE6755153228F736CA17A8142B74326995AA51D532DA56654B4B1ED4AD42EB3ACABFB4BA1690C6D741847E7B4A15933ECB920D28444CA8A2F06C14C8C512ABF74FF9F084AEA3A2E867F46EF0A42DFF32B89538CB11D3A03D67197F8FCB87AB75E9D96CAD9EAE956AD1474D378A1856D38D12EFA3A67F88665FD62B77654DCA1F54F641651F54F64165EFA5CAF650918DC43B684752741F1523C8B0708AB1FADF2051C721B4954F9712DDE2D2A3D5EF4387EE76877E889EB26AF85BFB9314DCC7EE1CDCB5B7B529C5F4A9F854A05CF02A76B05AE7A898E5F18AB88EF7C774DDE6F1628184B94EB7F6FF1C25F15C53279B6FB3FEB62FFB1739EAD98461E662CE5AE406CDB27C7E19A578A6E662EE85F2079DA28A766F993E2C74C2D1B9FD10CF505A0C4FE87C6384F2F86B10D1B074509696D1AC1C9CCE55BE88D2F85F91C582BD0E422C90D10D68083F4445799176B15FD59778F5D7E5D32D1B4D6A417ECCA3BDDDDDF2B5AEB4ADAEC6B52EBE8FB675BBCBAF818CE270736C6731C28A8B09B25588DAC20711F21521D75EDC1911326DC25073076FC3B452F2B92DD76EC600D9CACE3954A6D7FE39457193F1AAE441C0554FF82FA69D988154E47614F32DFE28D078B6CC1BD1A3D1EB37C8522CDC249340E5FD666DD759115BF497ABFB73373662AC0B9C20BB96533CAA7DFD0126854E156E309D2E6FB39BF4BE47352B306D1DAB4C5D05853CA87662815EE700289A9BB9A94A1ECCCD8ECCA7764D396E6D7E77132F1E4AFD40AB723F333117071A9F070E34A140071D609C7BBAEB0060D6092B89CE3AA06AA8A312A88A1EB400E458BBCCE6EB04F575ADD1CEED0B53F7D37D122D025A5ED240EB506B8AC1838DE482322D1581A4DA451990F3F606E3CB15FADC4ABF5A5DB98C564728053BCE17FA6B31135B0535D75555349DEBA22B48D983B2D891C5E2BB7491C4C54320E2665A9B38F17631CB70F1C1C94CB35C3E61B613CBB77769D5F73BB38F1364167951549B3AC14CD6FA8E6921AD3568EC91585650B1501148D382E5BCADD81AFFB21ADBAEB5062C98B175DEB3039BF1154A69670B9A52FAEAAB45BB9860B7A584D3FCC6B49C30D4D0DF14D396FB5864FAC9C1300F6598CD4470832D618D610C4498F548900D6FC0860EB710E9A762B47A5DAF8D865EA04803DDB85011EBEAB060911AD7D79BC8A17B38140E1A4947AB664F009F82D0CDDBD109CE9387E1C7856E996C1D48BE433DBC0FC2A1EAA2B3A2EB507E97E759FE215BB80DE3A6F43E0EE181CFC362D66CE4A427A6B3A91B2030292687C3532AA3D90696D3796E765A84398089C95CAC86DE72C544F0F040796E3C261B8C6F41FC0714A7284D55763C68E0A2F2DE1645368BEB014E6B2086658B957D97CE47C6AB34488D597437AE35D676F10AEB374CFB74FC8DD27C1D229BB2B5882C525CC47C7974743C96D5E3557A8E1254A2D1DB19B98CE92C2A66F5D14B5945E20A8829B5C8542A2D4ACEB0BDC23A3A4E4B55FDC6E92C5E4589A9F2D2478E5ABBAA14839773CED10AA595C23575830B5DEE3223953CA322F1CAC69A9309274E6629D3396F74D261F53FB572C22D750049D10BA0D559E440E21B79D4F933445DB1592B6C58BE0175663345F7B1E9E28B72E0CEF6C7A9BD211B18B4F67E73A944EBD7D8D600362F440CB2E4B82A11244A5C527B89AEA373C07DA8EC841C3BB56A33D2ECD49F2E558156E8BB22DD646DEA2166D24275286996F6E455327B21BD422BB623B5427FB9548139A5B62AA26E1305EDC113BBBCB8C8E33E4F08340DD8A010769B00B08F764204EB33392E62221ED009277EE2F1451137DB75D1E32BBF61C1E3FBC351EC9A93795B133AE1CCA84938E003A456E1B0C91C7C365D8025F14B015787CA817E4305F5115B421D5928A157EBF5A103C118407C557564469CA2BC61C24F550A7A84CEE8626AD41D5F50F797DC820A718A4ADECB558C47AD574CF45B290C103FA7EE130880B9A9DC20C8658206A0E676422B1CB9B101466A6E73B083D42C843188FBDE0241EE1882109A6B8A2C00C2BD22108E7451891B5C1D496D40A381D916B0291F77A340F123CA0E440FBC432864E8D82168700D0441B49A03043DEC036210736C076127AF209466E6E70AD31E0BD0A3B195936303CD98E262CC0ED96CF269E0DA1D43098A537692226A6F41E5CAF00A09F6C73B78E459F55B95A7586D9B0F9EC368F59E3C27121BE7D0709D0718608193B3D8CB5DCC3589276D608DCD3BEC84D8874DC0491E03A72C5E645F3F32D4BA7650B9B04DEF391E907396A31220FF3CBC9ADDFC9A627B25FD6466A59B27D3A7B3C2F0955A0A277E027E343F4F5A40FE89BE33003824BFAC23D7E8CE7175E8B834C2D58533F4C86C6379CCFC507D0B6EDE855EBC10FC091252168E0F62F823CC07FD72D76DC1EB507BB725AE0844EB1C820D6AB43AC809F31AD879152C36A39DD19B59A25DF7F6E34A733283AD7659DEC9843C7D45134E269A37B24E2EA3D52A4E17DC9B59346534250F669DFD61EAFFA0D492604C66029BE5B539A35466395E9049B9D5F50A73F43ECE8BF23C2AA3BBA83AA672365F2AC5DAB5BD66FADDD01196EF6A7F35D3F1A678F577B310648F86D145BEEAEDA25FBDC72D5956DEB2FAC89B3C8556BF1B558F9445380F385E8717DEEB65AAF7DAE9BFE68E68F02086931B7A2CEE152A1E8B4BF6A917BB9652AC174B76C76A1E9612B844D3DC518845CFA40AB5A9EE48ECE4200FC412DD71C8DB503C08497147E01F7EE271F8748F3E6BFC12428FC1CE0A138EF076138F2564A87827136968299E6365E82A3E77511138A909B65CEDAA2834CB6E0755A1FD721865D13CBDC1233469DB19DAADD35182025D911624E27A9480207FA41127D020681FB310FA8BA5FAF45AF55A85D867053011D1233451B63C4693E6C1192E0056600E97EE8EC6DEA3E0A158E2AE2987C601DF534550377D6745A1FBFEA02E0EEAE2A02E04946DA98B6683ADBBA6A0C2DC4549E83E1D463F54FF8BDF93945DEA0DB253D9A3336A85D0A92FE02F7FA35DD1ECF876ED09BA2FECDF11BA0F875A8E873376D3F6FE791E8B4B76C712AE9CE3D1848C6D2977E181171E4EC870C7E3AFACE7E1F8741FF7038DE015FD0F34D1A70FC8F32F22FB49DA70138E2D8D77E98046D7612F1EE3F01FFD96EF875102801CFBCE2A9FF59CB97D6645E0114BF5185220D2791724FA508A8043D33C38D45C132C70A849F4F0550AEF9C08FE4A21673BC624ACF2E71F35117C985CBA1F1A7BE844866319FB6E4AC4F368E23A4D7F52CDD8A77B6556E841BD9E568530A9B351D17CBEED097EE829E0967A9ADF73EDDACFDC594BFF5E367D7CE8E3507DACDBA177EEE0FA186CA7DE85BF1CC895FA8BEC47FDC54BE1F7178DF6F90B7181D5A47A4C25AAF72D84494495B0ADE903BB4B5385F29ABAB52F5608DDC45237EE3E0D3495A44F4A08FD550267986B84ADA90172DCA8871A804E7FB9A901F8CB5DD5F0BB3978F6CFEED0737E7D240E3AA2EA2872F0A7C3C81C3B9409ACFDF4A1A146BC26B44F42D345FCE9B1B89BF7056DD926EF92CCE8230A9C85063EB4EB2635BA6F7755550977A8F34042863B9EB23BEAB931DADC812EF084A679887F7D3DAB20F975CAA6E707CD95E6226349DABE9B93E66A73D14B42D2764F21B0B882DE7A41133FE2A31EB410BBAA25D80DCF3C084BF4389409DAB82E062E9493CE5D536C791A14428085F0C8EE93A20D8B2FBB9C45997D78CF89942B89C0A916973F9C406E4F9CDAA0D71EA2C442633B8991FEEB614488DD582B18E2DC7B538CBF915682EA70B49CBB74560283DF7AB061916B65652892EA85A44CE29A342F94EA5A5809E442B9FDC082D1DEFA2A21B5197E52A04CA258A2374E7DB32B0055A76F7EC08B61499A69901CA16D99EBC8C59D173C55BC954EFDC281DC2ABF9CF4C075545D46035F61C3D5A17BF5B457F9745653C68A9D65E9BC76668E2E8AEAC2617623B04BA3E5D8B42E722285311A7D795C398DCF4EE3C187F90F863C7664BCC1D3E12F18600865F78AE936F3F5150BD1B16A60A66D3FCEDCBDB490570FEBE238BBF3922286E9675D5CE86E75B512662A17619686A6B0DF2CCC9486780AB1A775ABAA48D2BA35050D3795633E4991F108D7FD6B3CAFE23D2F9FA6FF4C8EAAFCA3FACFB324C6CAA92D7119A5F13D368AE44D84F1EBA33F8F476F93382A4830308D667D23DF03E514DE7AFCAA0A6F45F3E544FEDC3F48B642298AB9B091ACBE2351C7596EFE5187BB7811574CB5BEDCE0FBC85D1B822A1252AEE5BA48E7E8F174FCEFFAC337A38B7F7C66DFBE185DE5B8A3DF8C5E8E7EE56BE072DB3E17B74A2A30C70D2BEBDFD7399AC5F58B1BE3971D1AC64EE111DCAF513E7B887253CB7E55DFB0F07E478604BC129235233D11C8DA3353ABADAF99D3D359E23B1A498691EAEBD7FC6048E86B3F0C3EEC35681BD941A8301DEE4252889575955FF7271D14ED03BFE5B0C7FAA78954D30BC26B7F39D8CAD06FCFEA8615EA48786B67E0A1D20FB50D840B0A4B22E3BAAB54F1F9DA2E2A4B7D5536909562B171AEADF3D514502CEA415F1CF4C5415F1CF485A42FA0B0D44DA88A41F404D9E0D48BD5B175C4FA704E0D223D30CECE3828E8739F6DD376EC08E764EAAEF28413803BA8F78410D3EECDE4E381BAAFD6A065BFF07C6CB71E2031A6DD11065FFE3A0F6C433CE73E8FEFBED2779867DA59CC824283C29E0F044B434FC332B6891E088A2A46A49A564FDED0410CD06096830F500DA9F28548D590C03B6CA3C4B0D5FDF2DDFA1A2F206EF4D9CEE9BB98954E33469FD7DAC1B0CE67DB057859B5837DA06EECEF6D07D4A19C6187C010C3AA0DF5ECB7C956477B9AC46D772C340B3DE86E98DA005067C7850B13033802ED93397F7F358D0C0DEE0204C32CF776C00FA2717779100C6E0DA06773F7553A84B04A8E8EC3B117EE53DDC117D72A342714BD2B403EEC459E0BDF1C429380EFD8EEABB40C61E985F8CAA06BA9F03B6A4D00665050126705CADE668C731388B9835E8B00E6A009C5EC31AB62A7CCFDF453F39DFBB1407FC5428EC9FD46F58B8B70B3E0CC7EEB88AE463288850CE0B2322899BE53656D24E4DE8A218BA9F4EB6BFA59DFC990127BD9715AC610362B7B3EB203873FEEF3DE188BA50CAAC4F8B0CA1E76BE8DA7EC074222297B6158E6661DB65F685465480783105ED9ABB9034C8DF8204B570757CF75AF2E8ECD6DC09A22D1C0D270CC907D946F794109EF03BA91271F7A90EFB093D0AF0B4D7166A17B91A3E5CD49F6EDFEF725170747AB55070BC10F68730F3CB217B849D5E99BF734ED729D94F12A89679820563F4747C74AE4E2557A8E1254A2D1DB1909443B8B8A19F4A67C15C3A7AB048913E3AB4053C40A7CA380D69AB7B2C2517296A558CD45B11A2A7E9DC7E92C5E4589D464A99CE304A26A0A439473CED10AA5D5B44068980B21C3F3862713062BB1D4D67C213CD22C2DBA28765D97B55E2BBEDBB8544076E4DE03D04890A30B62206130DD60165C1CC466BA103445EC6F522AE83A41F74A7377C11848A328BE0FB0322CF3198916B8CAD6D0D4DF25B529E9F278753E54EF0E2771829743AA929837ACBCF988407FB1B3DC9305CEF7AC57476D4BFEA06D3BB58FC1CEFD2DC999EEA6D89D902FED6D671B15AA7D3295EE42BDA7A6D15D5E8D57086F5C8080232662C765AAF090B4E72C3A9A9BD1B72E38FA3B94362436D62BA1764E7A386F21501196F51C24497FC1D77EC81374BD93D89B503742FDF77CE5C9B977B7224FFAFBB536234F4E179A891D2A853A349DC992372558925B1CAECEE0E2A57FFA69080933DF59A71332E31D735B92B37D505DBB21611B5460DDC46B833A4CBA38909D52972EF953C48ADEFBC86FC38C47EDAE8DB8C341AE083C1DCFEF32DCE764DB876629DE721198EDD228D02C070267996EF0F44E1C2D9126DF40AA296225D8C4DE02B49A2C980CFCF02B44819CBB8008901C0D3EC9B4C0D32B1A54F42603026FF22CD86294B84A42CA87284945DC08D2F0451D3D9A6D20073EDBA950E34DBA428BCF8428F1F9763A447F4244488E8602C9B4C393F513044F7234F024D3019EACAC417C92A5234072ED141ADF1344A2C9D3D068B25D893037A59E162B6224C94A3932D048582C6162A7075976880D24D9E66AC8B505DC24D028E7420183B8BBCAA43C37310C6017B2BAB12CDEF40B1E7218716578330B9F821066609235ADEF6DD6984AE13BC1BCD75FC1D65BBAA8D8A16953CD8E3CD0485D517DB595812EDD506F6AF254D8C576FABE4FF3D5AD6713072C1BD5E19920E912F8EDA220EC30EF95C24CF1D85F0DD73CF37E95086454A441D8442D9F137B40772FE8F0065AB1836CB00E1AE35655C8C1E2CEB69ECD2556CBDC58C8ED0BB9C6A5DA02F670E3CD845CFC9AD63A3C1011A8D1EA8C037AD662000650C78E0B03E0D709202F90D802A8EADB6480CE890AF3C0F10D098DD3556C8A66626673648120C3B2C428168EEF5684918C8198A1BC6AC1F24E26646A4D13F04FE5F58A93C9CD3AAD820BC8AF7354C48B16A27A54274533C119C6CA5CA4F759E396936AD4149163E25019CDA3327A9B97F17D342B71F60C15459CE2B5CCCF51B2C645DE2DEFD0FC22BD5A97AB75899B8C9677C913CF8CCAB767A27F3251EA7C7255C7F617219A50C73661BB7C95FEB08E9339ABF77BE074B606A2721AD200A2AA2FCBCACC2F9E18D2C72C7504A2EC63BECE5BB45C25D54B8757E934FA8ABAD40D8BDE07B488664FD7F40D123D88BD2344B69F9CC7D1228F9605C568BFC73FB10CCF978FDFFD1FA926C8808A0B0100, 
'6.2.0-61023');