#This build assumes the following directory structure
#
#  \Build          - This is where the project build code lives
#  \BuildArtifacts - This folder is created if it is missing and contains output of the build
#  \Code           - This folder contains the source code or solutions you want to build
#
Properties {
    $build_dir = Split-Path $psake.build_script_file    
    $build_artifacts_dir = "$build_dir\..\build-output\"
    $code_dir = "$build_dir\..\src"
    $solution = "NuGetGallery.MirrorJob.sln"
    $nuget = ".\nuget"
}

clear

FormatTaskName (("-"*25) + "[{0}]" + ("-"*25))

Task Default -Depends Full

Task Full -Depends Clean, Build, Test

Task Build -Depends Restore, Clean { 
    Write-Host "Building $solution" -ForegroundColor Green
    Exec { msbuild "$code_dir\$solution" /t:"Clean;Build" /p:Configuration=Release /v:minimal /p:OutDir=$build_artifacts_dir } 
}

Task Restore {
    Write-Host "Restoring nuget packages" -ForegroundColor Green
    Get-ChildItem $code_dir -Filter packages.config -Recurse | % {
        Write-Host $_.FullName -ForegroundColor DarkGreen
        &$nuget restore $_.FullName -PackagesDirectory $code_dir\packages
    }    
    Write-Host
}

Task Clean {
    Write-Host "Creating BuildArtifacts directory" -ForegroundColor Green
    if (Test-Path $build_artifacts_dir) 
    {   
        rd $build_artifacts_dir -rec -force | out-null
    }
    
    mkdir $build_artifacts_dir | out-null
    
    Write-Host "Cleaning $solution" -ForegroundColor Green
    Exec { msbuild "$code_dir\$solution" /t:Clean /p:Configuration=Release /v:quiet } 
}

Task Test -Depends TestTools {
    $nunit = Get-ChildItem tools -Recurse -Filter nunit-console.exe | Select-Object -First 1
    Write-Host using $nunit.FullName -ForegroundColor DarkGreen

    Get-ChildItem $build_artifacts_dir -Recurse -Filter *Tests.dll | % {
        &$nunit.FullName $_.FullName
    }
}

Task TestTools {
    if ((Get-ChildItem tools -Recurse -Filter nunit-console.exe).Length -eq 0)
    {
        Write-Host installing nunit.runners.lite -ForegroundColor DarkGreen
        &$nuget install nunit.runners.lite  -OutputDirectory tools
    }
    else
    {
        Write-Host nunit.runners.lite already installed -ForegroundColor DarkGreen
    }            
}