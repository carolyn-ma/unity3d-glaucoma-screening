fname = uigetfile();
fid = fopen(fname, 'r');
f =textscan(fopen(fname,'r'), '%f\t%f\t%f\n', 'HeaderLines',5);
x = f{1};
y = f{2};
z = f{3};
[xi,yi] = meshgrid(-30:0.1:30);
zi = griddata(x,y,z,xi,yi);
colormap jet;
pcolor (xi,yi,zi);
hold on;
plot([-30:0.5:30],0,'LineWidth',3,'k');
plot(0,[-30:0.5:30],'LineWidth',3,'k');
scatter(x,y,'w','filled','markeredgecolor','k');
shading interp;
axis([-30, 30, -30, 30]);
grid on;
xlabel ("Horizontal");
colorbar ("EastOutside");
caxis([0,4]); 
%text(-30, -35, 'Nasal','HorizontalAlignment','center', 'BackgroundColor', 'none');
%text(30, -35, 'Temporal','HorizontalAlignment','center', 'BackgroundColor', 'none');
text(40, 32, 'Major Vision Loss','HorizontalAlignment','center', 'BackgroundColor', 'none');
text(40, -32, 'No Vision Loss','HorizontalAlignment','center', 'BackgroundColor', 'none');
ylabel ("Perpendicular");
title ("Visual Field Heatmap");
hold off;
fclose(fname);