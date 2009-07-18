<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ServerModel>" %>
<%@ Import Namespace="Hudson.Models"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Hudson Monitor</title>
    
    <style type="text/css" media="all">@import "/css/hudson.css";</style>	
</head>
<body>
    <div class="container">
    
        <h1>
            Hudson Monitor: Jobs
        </h1>
    
        <fieldset>
        
            <p class="info-text">
                Available jobs on the server:
            </p>
    
            <% foreach (var job in Model.Jobs) { %>
            
                <p>
                    <a href="monitor/<%= HttpUtility.UrlEncode(job.Name) %>"><%= job.Name %></a>  (<%= job.BuildStatus %>)
                </p>
                
            <% } %>
        
            <p>
                <a href="/">&lt;&lt; Back</a>
            </p>
        
        </fieldset>
    </div>    
</body>
</html>
