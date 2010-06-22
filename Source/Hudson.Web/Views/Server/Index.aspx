<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Hudson.Web.Controllers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    
    <title>Hudson Monitor</title>
    
    <style type="text/css" media="all">@import "/css/hudson.css";</style>	
       
    <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" />  
    
    <script type="text/javascript" src="/js/jquery-1.2.6.min.js"></script>
    <script type="text/javascript" src="/js/hudson.js"></script>
    
</head>
<body>
    <div class="container">        
        
        <h1>
            Hudson Monitor
        </h1>
        
        <form method="post" action="/server/connect">
        
            <fieldset>
        
                <p class="info-text">
                    <% if (Session["error"] == null) { %>
                        Enter your <a href="https://hudson.dev.java.net/">Hudson</a> server details below:
                    <% } else { %>
                        <span class="error-text"><%= Session["error"] %></span>
                    <% } %>
                </p>
        
                <p>
                    <label for="url">URL:</label> 
                    <input type="text" name="url" id="url" value="<%= Settings.Server %>" />
                </p>
                
                <p>
                    <label for="username">Username:</label>
                    <input type="text" name="username" value="<%= Settings.Username %>" />
                </p>
                
                <p>            
                    <label for="password">Password:</label>
                    <input type="password" name="password" value="<%= Settings.Password %>" />
                </p>
                
                <p class="btn">
                    <input type="submit" value="Connect" />
                </p>
            
            </fieldset>
            
        </form>   
                
    </div>
    
    <p class="about">
        <a href="http://www.flipbit.co.uk/hudson-monitor.html" title="Hudson Monitor from flipbit.co.uk">Hudson Monitor</a> from flipbit
    </p>    
    
</body>
</html>
