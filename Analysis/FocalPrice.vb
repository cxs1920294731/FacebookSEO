﻿Imports HtmlAgilityPack
Imports System.Text.RegularExpressions

Public Class FocalPrice
    Private Shared listCategory As New List(Of Category)
    Private Shared listNowProductUrl As New List(Of String) '本期准备发送的产品

    Public Shared Sub Start(ByVal IssueID As Integer, ByVal siteId As Integer, ByVal planType As String, _
                         ByVal splitContactCount As Integer, ByVal spreadLogin As String, _
                         ByVal appId As String, ByVal url As String, ByVal nowTime As DateTime)
        Dim helper As New EFHelper
        listCategory = helper.GetListCategory(siteId)
        Dim pageUrl As String = "http://es.focalprice.com/"
        Dim doc As HtmlDocument = helper.GetHtmlDocument2(pageUrl, 120000)
        GetCategory(doc, nowTime, siteId)
        GetBanner1(siteId, doc, IssueID, nowTime)
        If (planType = "HA") Then  'planType="New Arrival"
            GetLatestNews(siteId, doc, IssueID, nowTime)
        ElseIf (planType = "DA") Then 'Daily Deals
            GetDeals(siteId, doc, IssueID, nowTime)
        End If
        GetCategoryProduct(siteId, IssueID, nowTime, planType)
        Dim subject As String = EFHelper.GetFirstProductSubject(IssueID)
        helper.InsertIssueSubject(IssueID, subject)
        helper.InsertContactList(IssueID, "ReasonableTest", "draft")
        helper.InsertContactList(IssueID, "ES小语种网站客户", "draft")
    End Sub

    ''' <summary>
    ''' 从页面中拿到分类信息，插入或者更新分类信息
    ''' </summary>
    ''' <param name="doc">分类信息所在页面链接</param>
    ''' <param name="lastUpdate">分类信息更新时间</param>
    ''' <param name="siteId">分类信息更新的账号</param>
    ''' <remarks></remarks>
    Private Shared Sub GetCategory(ByVal doc As HtmlDocument, ByVal lastUpdate As DateTime, ByVal siteId As Integer)
        Dim listInsertCategory As New List(Of Category) '需要插入的分类实体
        Dim listUpdateCategory As New List(Of Category) '需要更新的分类实体
        Dim listCategoryUrl As New List(Of String)
        For Each li In listCategory
            listCategoryUrl.Add(li.Url)
        Next
        Dim helper As New EFHelper
        Dim cate1 As New Category
        cate1.Category1 = "Acceosrios para Apple"
        cate1.Description = "Acceosrios para Apple"
        cate1.Url = "http://es.focalprice.com/apple-accessories/ca-001.html"
        cate1.ParentID = -1
        cate1.SiteID = siteId
        cate1.LastUpdate = lastUpdate
        If (listCategoryUrl.Contains(cate1.Url)) Then
            listUpdateCategory.Add(cate1)
        Else
            listInsertCategory.Add(cate1)
        End If

        Dim cate2 As New Category
        cate2.Category1 = "Celulares"
        cate2.Description = "Celulares"
        cate2.Url = "http://es.focalprice.com/cell-phones/ca-004.html"
        cate2.ParentID = -1
        cate2.SiteID = siteId
        cate2.LastUpdate = lastUpdate
        If (listCategoryUrl.Contains(cate2.Url)) Then
            listUpdateCategory.Add(cate2)
        Else
            listInsertCategory.Add(cate2)
        End If

        Dim cate3 As New Category
        cate3.Category1 = "Accesorios para Celulares"
        cate3.Description = "Accesorios para Celulares"
        cate3.Url = "http://dynamic.es.focalprice.com/cell-phone-accessories/ca-024.html"
        cate3.ParentID = -1
        cate3.SiteID = siteId
        cate3.LastUpdate = lastUpdate
        If (listCategoryUrl.Contains(cate3.Url)) Then
            listUpdateCategory.Add(cate3)
        Else
            listInsertCategory.Add(cate3)
        End If

        Dim cate4 As New Category
        cate4.Category1 = "Tablet PCs"
        cate4.Description = "Tablet PCs"
        cate4.Url = "http://es.focalprice.com/tablet-pcs/ca-013.html"
        cate4.ParentID = -1
        cate4.SiteID = siteId
        cate4.LastUpdate = lastUpdate
        If (listCategoryUrl.Contains(cate4.Url)) Then
            listUpdateCategory.Add(cate4)
        Else
            listInsertCategory.Add(cate4)
        End If

        Dim cate5 As New Category
        cate5.Category1 = "Accesorios para Autos"
        cate5.Description = "Accesorios para Autos"
        cate5.Url = "http://es.focalprice.com/cars-accessories/ca-003.html"
        cate5.ParentID = -1
        cate5.SiteID = siteId
        cate5.LastUpdate = lastUpdate
        If (listCategoryUrl.Contains(cate5.Url)) Then
            listUpdateCategory.Add(cate5)
        Else
            listInsertCategory.Add(cate5)
        End If

        Dim cate6 As New Category
        cate6.Category1 = "Electrónica de Consumo"
        cate6.Description = "Electrónica de Consumo"
        cate6.Url = "http://es.focalprice.com/consumer-electronics/ca-006.html"
        cate6.ParentID = -1
        cate6.SiteID = siteId
        cate6.LastUpdate = lastUpdate
        If (listCategoryUrl.Contains(cate6.Url)) Then
            listUpdateCategory.Add(cate6)
        Else
            listInsertCategory.Add(cate6)
        End If

        Dim cate7 As New Category
        cate7.Category1 = "Computadoras y Redes"
        cate7.Description = "Computadoras y Redes"
        cate7.Url = "http://es.focalprice.com/computers-networking/ca-005.html"
        cate7.ParentID = -1
        cate7.SiteID = siteId
        cate7.LastUpdate = lastUpdate
        If (listCategoryUrl.Contains(cate7.Url)) Then
            listUpdateCategory.Add(cate7)
        Else
            listInsertCategory.Add(cate7)
        End If

        Dim cate8 As New Category
        cate8.Category1 = "Relojes y Joyas"
        cate8.Description = "Relojes y Joyas"
        cate8.Url = "http://es.focalprice.com/apparel-accessories/ca-018.html"
        cate8.ParentID = -1
        cate8.SiteID = siteId
        cate8.LastUpdate = lastUpdate
        If (listCategoryUrl.Contains(cate8.Url)) Then
            listUpdateCategory.Add(cate8)
        Else
            listInsertCategory.Add(cate8)
        End If

        Dim cate9 As New Category
        cate9.Category1 = "Hogar y Jardín"
        cate9.Description = "Hogar y Jardín"
        cate9.Url = "http://es.focalprice.com/home-office/ca-008.html"
        cate9.ParentID = -1
        cate9.SiteID = siteId
        cate9.LastUpdate = lastUpdate
        If (listCategoryUrl.Contains(cate9.Url)) Then
            listUpdateCategory.Add(cate9)
        Else
            listInsertCategory.Add(cate9)
        End If

        Dim cate10 As New Category
        cate10.Category1 = "Juguetes, Hobbies & Games"
        cate10.Description = "Juguetes, Hobbies & Games"
        cate10.Url = "http://es.focalprice.com/toys-hobbies/ca-010.html"
        cate10.ParentID = -1
        cate10.SiteID = siteId
        cate10.LastUpdate = lastUpdate
        If (listCategoryUrl.Contains(cate10.Url)) Then
            listUpdateCategory.Add(cate10)
        Else
            listInsertCategory.Add(cate10)
        End If

        Dim cate11 As New Category
        cate11.Category1 = "Deportes y Al Aire Libre"
        cate11.Description = "Deportes y Al Aire Libre"
        cate11.Url = "http://es.focalprice.com/sports-outdoor/ca-014.html"
        cate11.ParentID = -1
        cate11.SiteID = siteId
        cate11.LastUpdate = lastUpdate
        If (listCategoryUrl.Contains(cate11.Url)) Then
            listUpdateCategory.Add(cate11)
        Else
            listInsertCategory.Add(cate11)
        End If

        Dim cate12 As New Category
        cate12.Category1 = "Salud, belleza y Vasos"
        cate12.Description = "Salud, belleza y Vasos"
        cate12.Url = "http://dynamic.es.focalprice.com/health-beauty/ca-016.html"
        cate12.ParentID = -1
        cate12.SiteID = siteId
        cate12.LastUpdate = lastUpdate
        If (listCategoryUrl.Contains(cate12.Url)) Then
            listUpdateCategory.Add(cate12)
        Else
            listInsertCategory.Add(cate12)
        End If
        'Dim categoryNodes As HtmlNodeCollection = doc.DocumentNode.SelectNodes("//div[@id='sidenav']/div[2]/div")
        'Dim counter As Integer = 0 '给Categor分类的个数计数
        'For Each node In categoryNodes
        '    Dim cate As New Category()
        '    cate.ParentID = -1
        '    cate.Category1 = node.SelectSingleNode("span/a").InnerText.Trim()
        '    'Dim categoryName As String = node.SelectSingleNode("span/a").InnerText
        '    cate.SiteID = siteId
        '    cate.LastUpdate = lastUpdate
        '    cate.Description = node.SelectSingleNode("span/a").InnerText.Trim()
        '    Dim categoryUrl As String = ParseLink(node.SelectSingleNode("span/a").GetAttributeValue("href", "").Trim())
        '    cate.Url = categoryUrl
        '    If (listCategoryUrl.Contains(categoryUrl)) Then
        '        listUpdateCategory.Add(cate)
        '    Else
        '        listInsertCategory.Add(cate)
        '    End If
        '    counter = counter + 1
        '    If (counter >= categoryNodes.Count - 1) Then
        '        Exit For
        '    End If
        'Next
        If Not (listCategory.Count > 0) Then  '不是第一次添加分类
            Dim cateBanner As New Category
            cateBanner.Category1 = "Banner"
            cateBanner.ParentID = -1
            cateBanner.SiteID = siteId
            cateBanner.LastUpdate = lastUpdate
            helper.InsertCategory(cateBanner)
        End If
        If (listUpdateCategory.Count > 0) Then
            helper.UpdateListCategory(listUpdateCategory)
        End If
        If (listInsertCategory.Count > 0) Then
            helper.InsertListCategory(listInsertCategory)
        End If
    End Sub

    ''' <summary>
    ''' 获取页面的Banner条信息，插入或者更新信息
    ''' </summary>
    ''' <param name="siteId"></param>
    ''' <param name="doc1"></param>
    ''' <param name="issueId"></param>
    ''' <param name="lastUpdate"></param>
    ''' <remarks></remarks>
    Public Shared Sub GetBanner1(ByVal siteId As Integer, ByVal doc1 As HtmlDocument, ByVal issueId As Integer, ByVal lastUpdate As DateTime)
        Dim helper As New EFHelper()
        Dim iframeurl As String = "http://promotion.focalprice.com/Slide/es_index770_v45.html"
        Dim iframecontext As String = helper.GetHtmlDocument2(iframeurl, 120000).DocumentNode.InnerHtml
        Dim slotnamelist As List(Of String) = New List(Of String)()
        Dim clientlist As List(Of String) = New List(Of String)()
        For Each Str As String In Regex.Split(iframecontext, "GA_googleAddSlot\(")
            If Str.First <> "<" Then
                Dim client As String = Regex.Split(Str, """")(1)
                Dim slotname As String = Regex.Split(Str, """")(3)
                slotnamelist.Add(slotname)
                clientlist.Add(client)
            End If
        Next

        Dim urlpre As String = "http://pubads.g.doubleclick.net/gampad/ads?correlator=978868044300288&output=json_html&callback=GA_googleSetAdContentsBySlotForSync&impl=s&client="
        Dim imageUrlList As List(Of String) = New List(Of String)
        Dim aimUrlList As List(Of String) = New List(Of String)
        Dim pagetxt As String
        Dim imageURL As String
        Dim aim As String
        Dim clientstep As Integer = 0
        For Each slot As String In slotnamelist
            Try

                Dim url As String = urlpre + clientlist.Item(clientstep) + "&slotname=" + slot
                clientstep = clientstep + 1
                pagetxt = helper.GetHtmlDocument2(url, 120000).DocumentNode.InnerHtml
                imageURL = "http://static.googleadsserving.cn/pagead/imgad?id="

                imageURL = imageURL + Regex.Split(Regex.Split(pagetxt, "imgad\?id\\x3d")(1), "\\x22")(0)

                aim = "http://googleads.g.doubleclick.net/aclk?sa"
                aim = aim + Regex.Split(Regex.Split(pagetxt, "http://googleads\.g\.doubleclick\.net/aclk\?sa")(1).Replace("\x3d", "=").Replace("\x26", "&"), "\\x22")(0)
                aim = Regex.Split(Regex.Split(aim, "adurl=")(1), "%3")(0)
                imageUrlList.Add(imageURL)
                aimUrlList.Add(aim)
            Catch ex As Exception

            End Try

        Next
        Dim imagestep As Integer = 0
        Dim listUpdateAds As New List(Of Ad)
        Dim listInsertAds As New List(Of Ad)
        Dim listAdsId As New List(Of Integer)
        Dim listAdsLink As New List(Of String)
        For Each li In helper.GetListAd(siteId)
            listAdsLink.Add(li.Url)
        Next

        For Each node In imageUrlList
            Dim ad As New Ad
            Dim adLink As String = aimUrlList.Item(imagestep)
            imagestep = imagestep + 1
            ad.Url = adLink
            ad.PictureUrl = node
            ad.SiteID = siteId
            ad.Lastupdate = lastUpdate
            If (listAdsLink.Contains(adLink)) Then
                listUpdateAds.Add(ad)
            Else
                listInsertAds.Add(ad)
            End If

        Next
        If (listInsertAds.Count > 0) Then
            listAdsId.AddRange(helper.InsertAds(listInsertAds, siteId))
        End If
        If (listUpdateAds.Count > 0) Then
            listAdsId.AddRange(helper.UpdateAds(listUpdateAds, siteId))
        End If
        helper.InsertAdsIssue2(listAdsId, siteId, issueId, listUpdateAds.Count + listInsertAds.Count, 1)
    End Sub

    ''' <summary>
    ''' 获取页面的Banner条信息，插入或者更新信息
    ''' </summary>
    ''' <param name="siteId"></param>
    ''' <param name="doc1"></param>
    ''' <param name="issueId"></param>
    ''' <param name="lastUpdate"></param>
    ''' <remarks></remarks>
    Public Shared Sub GetBanner(ByVal siteId As Integer, ByVal doc1 As HtmlDocument, ByVal issueId As Integer, ByVal lastUpdate As DateTime)
        Dim helper As New EFHelper()
        Dim bannerUrl As String = "http://special.focalprice.com/ajaxApi-getHomeBanner-lang-es"  'Banner动态加载外部html
        Dim jsBanner As String = helper.GetHtmlDocument2(bannerUrl, 120000).DocumentNode.InnerHtml
        Dim reg As String = "^(document.write\(\')(.*)(\'\);)$"
        Dim strBanner As String = Regex.Matches(jsBanner, reg)(0).Groups(2).Value
        Dim doc As New HtmlDocument
        doc.LoadHtml(strBanner)
        Dim listAdsLink As New List(Of String)
        For Each li In helper.GetListAd(siteId)
            listAdsLink.Add(li.Url)
        Next
        Dim listUpdateAds As New List(Of Ad)
        Dim listInsertAds As New List(Of Ad)
        Dim listAdsId As New List(Of Integer)
        Dim adsNodes As HtmlNodeCollection = doc.DocumentNode.SelectNodes("//div[@id='focus']/ul/li")
        For Each node In adsNodes
            Dim ad As New Ad
            Dim adLink As String = node.SelectSingleNode("a").GetAttributeValue("href", "").Trim()
            ad.Url = adLink
            ad.PictureUrl = node.SelectSingleNode("a/img").GetAttributeValue("src", "").Trim()
            ad.SiteID = siteId
            ad.Lastupdate = lastUpdate
            If (listAdsLink.Contains(adLink)) Then
                listUpdateAds.Add(ad)
            Else
                listInsertAds.Add(ad)
            End If
        Next
        If (listInsertAds.Count > 0) Then
            listAdsId.AddRange(helper.InsertAds(listInsertAds, siteId))
        End If
        If (listUpdateAds.Count > 0) Then
            listAdsId.AddRange(helper.UpdateAds(listUpdateAds, siteId))
        End If
        helper.InsertAdsIssue2(listAdsId, siteId, issueId, listUpdateAds.Count + listInsertAds.Count, 2)
    End Sub

    ''' <summary>
    ''' 获取页面的Ofertas Diarias信息，插入或者更新信息
    ''' Sections表中SectionID="DA",Daily Deals
    ''' </summary>
    ''' <param name="siteId"></param>
    ''' <param name="doc"></param>
    ''' <param name="issueId"></param>
    ''' <param name="lastUpdae"></param>
    ''' <remarks></remarks>
    Public Shared Sub GetDeals(ByVal siteId As Integer, ByVal doc As HtmlDocument, ByVal issueId As Integer, ByVal lastUpdae As DateTime)
        Dim helper As New EFHelper
        'Dim listProductUrl As List(Of String) = helper.GetListProduct(siteId) 'Products表中的产品URL
        Dim productNodes As HtmlNodeCollection = doc.DocumentNode.SelectNodes("//div[@class='dailydeals_warp']/div[1]/div")
        'Dim p1 As Integer = -1
        Dim counter As Integer = 0  '产品计数器
        For Each myNode In productNodes
            'Dim random As New Random
            'Dim i As Integer = random.Next(0, 4)
            'While (i = p1)
            '    i = random.Next(0, 4)
            'End While
            'p1 = i  '记录上一次的i的值
            Dim node As HtmlNode = myNode   'productNodes(i)
            Dim pNode As HtmlNode = node.SelectSingleNode("ul")
            Dim product As New Product
            Dim productName As String = pNode.SelectSingleNode("li[2]").InnerText.Trim()  '产品名2行，70个字符，多了就截取掉
            If (productName.Length >= 63) Then
                productName = productName.Substring(0, 63).Trim()
                productName = productName.Substring(0, productName.LastIndexOf(" ")) & "..."
            End If
            product.Prodouct = productName
            Dim productUrl As String = pNode.SelectSingleNode("li[1]/a").GetAttributeValue("href", "").Trim()
            product.Url = productUrl
            Dim strPrice As String = pNode.SelectSingleNode("li[3]").InnerText   ' US$ 93.99     US$ 115.99
            product.Price = Double.Parse(Regex.Split(strPrice, "US\$")(2).Trim())  '原价
            product.Discount = Double.Parse(Regex.Split(strPrice, "US\$")(1).Trim())  '现价
            product.PictureUrl = pNode.SelectSingleNode("li[1]/a/img").GetAttributeValue("src", "")
            product.SiteID = siteId
            Dim docProduct As HtmlDocument = helper.GetHtmlDocument2(product.Url, 120000)
            Dim cateUrl As String = docProduct.DocumentNode.SelectSingleNode("//div[@id='breadcrumbs']/div[1]/h3/span/a[2]").GetAttributeValue("href", "").Trim()
            Dim categoryName As String = docProduct.DocumentNode.SelectSingleNode("//div[@id='breadcrumbs']/div[1]/h3/span/a[2]").InnerText.Trim()
            If Not (helper.IsProductSent(siteId, productUrl, Now.AddDays(-15).ToString(), Now)) Then '该产品在前几期中还未发送
                Dim productId As Integer = helper.InsertOrUpdateProduct(product, cateUrl, siteId, lastUpdae) 'helper.InsertSingleProduct2(product, cateUrl, siteId)
                helper.InsertSinglePIssue(productId, siteId, issueId, "DA")
            Else
                Continue For
            End If

            'If (listProductUrl.Contains(productUrl)) Then '产品已经存在产品表中
            '    Dim productId As Integer = helper.UpdateSingleProduct2(product, cateUrl, siteId, lastUpdae)
            '    helper.InsertSinglePIssue(productId, siteId, issueId, "DA")
            'Else  '第一次添加产品
            'End If

            listNowProductUrl.Add(product.Url)
            counter = counter + 1
            If (counter >= 2) Then '每次存放2个产品
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' 获取"Últimas Novedades",New Arrival块的2个产品信息，并填充到数据库中；
    ''' 首页产品不更新，则产品每3期不重复；首页产品更新，则产品每期都不重复；
    ''' </summary>
    ''' <param name="siteId"></param>
    ''' <param name="doc"></param>
    ''' <param name="issueId"></param>
    ''' <param name="lastUpdate"></param>
    ''' <remarks></remarks>
    Private Shared Sub GetLatestNews(ByVal siteId As Integer, ByVal doc As HtmlDocument, ByVal issueId As Integer, ByVal lastUpdate As DateTime)
        Dim helper As New EFHelper
        Dim listProductUrl As List(Of String) = helper.GetListProduct(siteId) 'Products表中的产品URL
        Dim productNodes As HtmlNodeCollection = doc.DocumentNode.SelectNodes("//div[@class='hotitem mt10 Recommended_item']/div[2]/div[1]/div")  'doc.DocumentNode.SelectNodes("//div[@class='hotitem mt10 featured_item']/div[2]/div[1]/div")
        Dim listLastProdUrl As List(Of String) = helper.GetTopNSectionProdUrl(issueId, 2, "NE", siteId)
        Dim counter As Integer = 0
        For Each node In productNodes
            Dim productUrl As String = ParseLink(node.SelectSingleNode("ul/li[1]/a").GetAttributeValue("href", "").Replace("&#39;", "'").Trim())
            If (listLastProdUrl.Contains(productUrl)) Then
                Continue For
            Else
                Dim product As New Product
                Dim productName As String = node.SelectSingleNode("ul/li[2]/a").InnerText.Trim()
                If (productName.Length >= 68) Then
                    productName = productName.Substring(0, 68).Trim()
                    productName = productName.Substring(0, productName.LastIndexOf(" ")) & "..."
                End If
                product.Prodouct = productName
                product.Url = productUrl
                product.Discount = Double.Parse(Regex.Split(node.SelectSingleNode("ul/li[3]/span[1]").InnerText.Trim(), "US\$")(1).Trim()) '折扣价
                Try
                    product.Price = Double.Parse(Regex.Split(node.SelectSingleNode("ul/li[3]/s[1]").InnerText.Trim(), "US\$")(1).Trim()) '原价，划了横线的价格
                Catch ex As Exception
                    'Ignore
                End Try
                'product.Price = Double.Parse(Regex.Split(node.SelectSingleNode("ul/li[3]/s[1]").InnerText.Trim(), "US\$")(1).Trim()) '原价，划了横线的价格
                product.PictureUrl = node.SelectSingleNode("ul/li[1]/a/img").GetAttributeValue("src", "").Trim()
                product.LastUpdate = lastUpdate
                product.SiteID = siteId
                Dim docProduct As HtmlDocument = helper.GetHtmlDocument2(productUrl, 120000)
                Dim cateUrl As String = docProduct.DocumentNode.SelectSingleNode("//div[@id='breadcrumbs']/div[1]/h3/span/a[2]").GetAttributeValue("href", "").Trim()
                Dim categoryName As String = docProduct.DocumentNode.SelectSingleNode("//div[@id='breadcrumbs']/div[1]/h3/span/a[2]").InnerText.Trim()
                If Not (helper.IsProductSent(siteId, productUrl, Now.AddDays(-15).ToString(), Now)) Then '该产品在前几期中还未发送
                    Dim productId As Integer = helper.InsertOrUpdateProduct(product, cateUrl, siteId, lastUpdate) 'helper.InsertSingleProduct2(product, cateUrl, siteId)
                    'helper.InsertSinglePIssue(productId, siteId, issueId, "DA")
                    helper.InsertSinglePIssue(productId, siteId, issueId, "NE")
                    listNowProductUrl.Add(product.Url)
                    counter = counter + 1
                Else
                    Continue For
                End If
                If (counter >= 2) Then
                    Exit For
                End If
            End If
        Next
        If Not (counter >= 2) Then
            productNodes = doc.DocumentNode.SelectNodes("//div[@class='hotitem mt10 Recommended_item']/div[3]/div[2]/div")
            For Each node In productNodes
                Dim productUrl As String = ParseLink(node.SelectSingleNode("ul/li[1]/a").GetAttributeValue("href", "").Trim())
                If (listLastProdUrl.Contains(productUrl)) Then
                    Continue For
                Else
                    Dim product As New Product
                    Dim productName As String = node.SelectSingleNode("ul/li[2]/a").InnerText.Trim()
                    If (productName.Length >= 68) Then
                        productName = productName.Substring(0, 68).Trim()
                        productName = productName.Substring(0, productName.LastIndexOf(" ")) & "..."
                    End If
                    product.Prodouct = productName
                    product.Url = productUrl
                    product.Discount = Double.Parse(Regex.Split(node.SelectSingleNode("ul/li[3]/span[1]").InnerText.Trim(), "US\$")(1).Trim()) '折扣价
                    product.Price = Double.Parse(Regex.Split(node.SelectSingleNode("ul/li[3]/s[1]").InnerText.Trim(), "US\$")(2).Trim()) '原价，划了横线的价格
                    product.PictureUrl = node.SelectSingleNode("ul/li[1]/a/img").GetAttributeValue("src", "").Trim()
                    product.LastUpdate = lastUpdate
                    product.SiteID = siteId
                    Dim docProduct As HtmlDocument = helper.GetHtmlDocument2(productUrl, 120000)
                    Dim cateUrl As String = docProduct.DocumentNode.SelectSingleNode("//div[@id='breadcrumbs']/div[1]/h3/span/a[2]").GetAttributeValue("href", "").Trim()
                    Dim categoryName As String = docProduct.DocumentNode.SelectSingleNode("//div[@id='breadcrumbs']/div[1]/h3/span/a[2]").InnerText.Trim()
                    If Not (helper.IsProductSent(siteId, productUrl, Now.AddDays(-15).ToString(), Now)) Then '该产品在前几期中还未发送
                        Dim productId As Integer = helper.InsertOrUpdateProduct(product, cateUrl, siteId, lastUpdate) 'helper.InsertSingleProduct2(product, cateUrl, siteId)
                        'helper.InsertSinglePIssue(productId, siteId, issueId, "DA")
                        helper.InsertSinglePIssue(productId, siteId, issueId, "NE")
                        listNowProductUrl.Add(product.Url)
                        counter = counter + 1
                    Else
                        Continue For
                    End If
                    If (counter >= 2) Then
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' 从每个分类中拿取"Categorías Recomendación"块的一个产品
    ''' </summary>
    ''' <param name="siteId"></param>
    ''' <param name="issueId"></param>
    ''' <param name="lastUpdate"></param>
    ''' <remarks></remarks>
    Private Shared Sub GetCategoryProduct(ByVal siteId As Integer, ByVal issueId As Integer, ByVal lastUpdate As DateTime, _
                                          ByVal planType As String)
        Dim listSKU As New List(Of String)
        Dim helper As New EFHelper
        'Dim listmyCategory As List(Of Category) = helper.GetListCategory(siteId)
        '获取固定分类的几个产品
        Dim listCategoryUrl As New List(Of String)
        listCategoryUrl.Add("http://es.focalprice.com/apple-accessories/ca-001.html")
        listCategoryUrl.Add("http://es.focalprice.com/cell-phones/ca-004.html")
        listCategoryUrl.Add("http://es.focalprice.com/tablet-pcs/ca-013.html")
        listCategoryUrl.Add("http://es.focalprice.com/consumer-electronics/ca-006.html")
        listCategoryUrl.Add("http://es.focalprice.com/apparel-accessories/ca-018.html")
        listCategoryUrl.Add("http://es.focalprice.com/toys-hobbies/ca-010.html")

        Dim listLastProdUrl As New List(Of String)

        'Dim listLastAllProdUrl As New List(Of String)
        'listLastAllProdUrl = helper.GetTopNSectionProdUrl(issueId, 26, "", siteId)
        'Dim iTotalCounter As Integer = 36

        For Each url In listCategoryUrl '取Top Seller块的产品，如果不够则获取Recomendar处的产品, For Each li In listmyCategory
            If Not (String.IsNullOrEmpty(url)) Then '如果该分类不是首页的分类，则不作处理
                Dim listProductId As New List(Of Integer)
                Dim doc As HtmlDocument = helper.GetHtmlDocument2(url, 120000)
                Dim cateProdNodes As HtmlNodeCollection '= doc.DocumentNode.SelectNodes("//div[@class='col_m mt10']/div[4]/ul/li") 'doc.DocumentNode.SelectNodes("//div[@class='hotitem bt_border mt10 Recommended_item']/div[2]/div[1]/div")
                Dim requestMoreUrl As String = ""
                If (planType = "HA") Then 'New Arrival
                    Try
                        requestMoreUrl = doc.DocumentNode.SelectSingleNode("//div[@class='col_m mt10']/div[3]/h4/a").GetAttributeValue("href", "")
                    Catch ex As Exception
                        'Ignore
                    End Try
                ElseIf (planType = "DA") Then 'Daily Deal
                    Try
                        requestMoreUrl = doc.DocumentNode.SelectSingleNode("//div[@class='col_m mt10']/div[4]/h4/a").GetAttributeValue("href", "")
                    Catch ex As Exception
                        'Ignore
                    End Try
                Else '分类促发
                    Try
                        requestMoreUrl = doc.DocumentNode.SelectSingleNode("//div[@class='col_m mt10']/div[5]/h4/a").GetAttributeValue("href", "")
                    Catch ex As Exception
                        'Ignore
                    End Try
                End If
                '几个分类没有more的情况,begin
                If (url = "http://es.focalprice.com/tablet-pcs/ca-013.html") Then
                    requestMoreUrl = "http://dynamic.es.focalprice.com/categorylist/013001?pagesize=72"
                End If
                If (url = "http://es.focalprice.com/consumer-electronics/ca-006.html") Then
                    requestMoreUrl = "http://dynamic.es.focalprice.com/categorylist/006013?pagesize=72"
                End If
                If (url = "http://es.focalprice.com/toys-hobbies/ca-010.html") Then
                    requestMoreUrl = "http://dynamic.es.focalprice.com/categorylist/010006?pagesize=72"
                End If
                'end
                cateProdNodes = helper.GetHtmlDocument2(requestMoreUrl, 120000).DocumentNode.SelectNodes("//div[@id='list_content']/div")
                Dim counter As Integer = 0
                For Each cateProdNode In cateProdNodes
                    Dim productUlNode As HtmlNode = cateProdNode.SelectSingleNode("ul")
                    'For Each liNode As HtmlNode In productLiNode
                    Dim product As New Product
                    Dim productName As String = productUlNode.SelectSingleNode("li[@class='proName f11']").InnerText.Trim()  'cateProdNode.SelectSingleNode("p[@class='listProduct_name']").InnerText.Trim()
                    Dim productUrl As String = ParseLink(productUlNode.SelectSingleNode("li[@class='proName f11']/a").GetAttributeValue("href", "").Trim())  'ParseLink(cateProdNode.SelectSingleNode("p[@class='listProduct_img']/a").GetAttributeValue("href", "").Trim())
                    If (counter >= 3) Then '插入一个产品
                        Exit For
                    Else
                        If Not (listNowProductUrl.Contains(productUrl)) Then '该产品在其他模块中没有出现
                            If Not (helper.IsProductSent(siteId, productUrl, Now.AddDays(-30).ToString, Now.ToString())) Then '产品在前7天中没有发送过
                                If (productName.Length >= 63) Then
                                    productName = productName.Substring(0, 63).Trim()
                                    productName = productName.Substring(0, productName.LastIndexOf(" ")) & "..."
                                End If
                                product.Prodouct = productName
                                product.Url = productUrl 'proPri
                                '2014-04-09,添加sku 产品重复判断,begin
                                'Julie:提醒一下前面不一定是6位，有可能是7倍或者5位，总之就是只有最后一位代码不一样的，前面全部相同的，都只拿一个
                                Dim firstIndex As Integer = 0 ' href.LastIndexOf("/", href.LastIndexOf("/") - 1) + 1
                                Dim lastIndex As Integer = 0 ' href.LastIndexOf("/")
                                Dim skuName As String = "" ' href.Substring(firstIndex, lastIndex - firstIndex)
                                firstIndex = productUrl.LastIndexOf("/", productUrl.LastIndexOf("/") - 1) + 1
                                lastIndex = productUrl.LastIndexOf("/")
                                skuName = productUrl.Substring(firstIndex, lastIndex - firstIndex)
                                skuName = skuName.Substring(0, skuName.Length - 1)
                                If (listSKU.Contains(skuName)) Then
                                    Continue For
                                Else
                                    listSKU.Add(skuName)
                                End If
                                'end

                                product.Discount = Double.Parse(Regex.Split(productUlNode.SelectSingleNode("li[@class='proPri']/span[1]").InnerText.Trim(), "US\$")(1).Trim())  'Double.Parse(Regex.Split(cateProdNode.SelectSingleNode("p[@class='listProduct_price']/span[1]").InnerText.Trim(), "US\$")(1).Trim())
                                product.PictureUrl = productUlNode.SelectSingleNode("li[@class='proImg']/a/img").GetAttributeValue("src", "")
                                product.LastUpdate = lastUpdate
                                product.SiteID = siteId
                                Dim productId As Integer = helper.InsertOrUpdateProduct(product, url.Trim(), siteId, lastUpdate)
                                helper.InsertSinglePIssue(productId, siteId, issueId, "CA")
                                listNowProductUrl.Add(product.Url)
                                counter = counter + 1
                            End If
                        End If
                    End If
                    'Next

                    '2014/04/08 added，拿more页的产品信息
                    'Dim product As New Product
                    'Dim productName As String = cateProdNode.SelectSingleNode("p[@class='listProduct_name']").InnerText.Trim()
                    'Dim productUrl As String = ParseLink(cateProdNode.SelectSingleNode("p[@class='listProduct_img']/a").GetAttributeValue("href", "").Trim())
                    'If (counter >= 3) Then '插入一个产品
                    '    Exit For
                    'Else
                    '    If Not (listNowProductUrl.Contains(productUrl)) Then '该产品在其他模块中没有出现
                    '        If Not (helper.IsProductSent(siteId, productUrl, Now.AddDays(-9).ToString, Now.ToString())) Then '产品在前7天中没有发送过
                    '            If (productName.Length >= 63) Then
                    '                productName = productName.Substring(0, 63).Trim()
                    '                productName = productName.Substring(0, productName.LastIndexOf(" ")) & "..."
                    '            End If
                    '            product.Prodouct = productName
                    '            product.Url = productUrl
                    '            product.Discount = Double.Parse(Regex.Split(cateProdNode.SelectSingleNode("p[@class='listProduct_price']/span[1]").InnerText.Trim(), "US\$")(1).Trim())
                    '            product.PictureUrl = cateProdNode.SelectSingleNode("p[@class='listProduct_img']/a/img").GetAttributeValue("src", "")
                    '            product.LastUpdate = lastUpdate
                    '            product.SiteID = siteId
                    '            Dim productId As Integer = helper.InsertOrUpdateProduct(product, url.Trim(), siteId, lastUpdate)
                    '            helper.InsertSinglePIssue(productId, siteId, issueId, "CA")
                    '            listNowProductUrl.Add(product.Url)
                    '            counter = counter + 1
                    '        End If
                    '    End If
                    'End If
                Next
                'helper.InsertProductIssue(listProductId, siteId, issueId, "CA", 0, 36)
            End If
        Next
    End Sub

    ''' <summary>
    ''' 链接转换，添加域名
    ''' </summary>
    ''' <param name="link"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ParseLink(ByVal link As String)
        If Not (link.Contains("es.focalprice.com")) Then
            If (link.Substring(0).Contains("/")) Then
                link = "http://es.focalprice.com" & link
            Else
                link = "http://es.focalprice.com/" & link
            End If
        End If
        link = link.Replace("&#39;", "'")
        Return link
    End Function
End Class
