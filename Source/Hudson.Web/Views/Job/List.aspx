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
            
                <p class="status <%= job.BuildStatus.ToString().ToLower() %>">
                    <a href="monitor/<%= HttpUtility.UrlEncode(job.Name) %>"><%= job.Name %></a>  (<%= job.BuildStatus %>)
                </p>
                
            <% } %>
        
            <p class="btn" style="padding: 0">
                <a href="/">Main Menu</a>
            </p>
        
        </fieldset>
    </div>    
    <p class="about">
        <a href="http://www.flipbit.co.uk/hudson-monitor.html" title="Hudson Monitor from flipbit.co.uk">Hudson Monitor</a> from flipbit
    </p>     
</body>
</html>
