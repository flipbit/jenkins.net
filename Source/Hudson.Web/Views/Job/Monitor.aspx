<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<BuildModel>" %>
<%@ Import Namespace="Hudson.Web.Helpers"%>
<%@ Import Namespace="Hudson.Web.Models"%>
<html>
    <head>
    
        <style type="text/css" media="all">@import "/css/hudson.css";</style>	
        
        <script type="text/javascript" src="/js/jquery-1.2.6.min.js"></script>
        <script type="text/javascript" src="/js/hudson.js"></script>
        
        <meta http-equiv="refresh" content="60" />
        
        <title>Hudson Status - <%= Model.Name %></title>
        
    </head>
    <body class="<%= Model.Status.ToLower() %>">
        <p>
            <span class="label">
                Build:
            </span>
            <span class="info">
                <%= Html.Encode(Model.Number) %>
            </span>
        </p>
        <p>
            <span class="label">
                Status:
            </span>
            <span class="info">
                <%= Html.Encode(Model.Status) %>
            </span>            
        </p>
        <p>
            <span class="label">
                Revision:
            </span>
            <span class="info">
                <%= Html.Encode(Model.Revision) %>
            </span>            
        </p>
        <p>
            <span class="label">
                Build:
            </span>
            <span class="info">
                <%= Html.Encode(Model.Created.ToString("MMM dd")) %> 
                at
                <%= Html.Encode(Model.Created.ToString("HH:mm")) %> 
            </span>            
        </p>
        <p class="double">
            <span class="label">
                Culprit:
            </span>
            <span class="info">
                <img src="/images/<%= Html.UserImage(Model.User) %>.png" alt="<%= Model.User %>'s Avatar" title="<%= Model.User %>'s Avatar" />
            </span>            
        </p>
        <p class="double">
            <span class="label">
                Comments:
            </span>
            <span class="info">
                <%= Html.Encode(Model.Comments) %>
            </span>            
        </p>
    </body>
</html>
