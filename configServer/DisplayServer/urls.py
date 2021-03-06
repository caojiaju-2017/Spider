#!/usr/bin/env python
# -*- coding: utf-8 -*-

"""DisplayServer URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/1.10/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  url(r'^$', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  url(r'^$', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.conf.urls import url, include
    2. Add a URL to urlpatterns:  url(r'^blog/', include('blog.urls'))
"""
from django.conf.urls import url
from django.contrib import admin
from django.conf import settings
from django.conf.urls.static import static

from DSServer.Api.WebCenterApi import *
from DSServer.Api.AppServiceApi import *

urlpatterns = [
    # API接口
    url(r'^admin/', admin.site.urls),
    url(r'^$',WebCenterApi.goHome),
    url(r'^home.html',WebCenterApi.goHome),
    url(r'^login.html',WebCenterApi.openLogin),
    url(r'^excuteLogin',WebCenterApi.excuteLogin),
    url(r'^search_data',WebCenterApi.searchData),
    # url(r'^searchService.html',WebCenterApi.serviceQuery),
    url(r'^ImageView.html',WebCenterApi.openImageView),
    url(r'^my_order.html',WebCenterApi.openMyOrder),
    url(r'^getMyOrderData',WebCenterApi.getMyOrderData),
    url(r'^intro.html',WebCenterApi.openIntro),
    url(r'^reportIntro.html',WebCenterApi.openReportIntro),
    url(r'^my_service1.html',WebCenterApi.serviceDataQuery),
    url(r'^getServiceData',WebCenterApi.getServiceDataData),

    url(r'^my_service2.html',WebCenterApi.serviceDataQuery2),
    url(r'^api/account/$', AppServiceApi.CommandDispatch),

] + static(settings.STATIC_URL, document_root = settings.STATIC_ROOT)